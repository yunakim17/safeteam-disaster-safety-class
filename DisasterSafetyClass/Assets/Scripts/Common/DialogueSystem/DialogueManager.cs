using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // �ڸ� �ؽ�Ʈ ǥ�ÿ�
    public AudioSource audioSource; // ���� �����
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�

    private bool isTyping = false; //���� Ÿ���� �� ����
    private string currentText = ""; // ���� ������� ��� �ؽ�Ʈ
    private int currentLineIndex = 0; // ���� �� ��° �������
    private List<DialogueLine> dialogueLines; //��縮��Ʈ
    private Dictionary<int, ChoiceEntry> choiceDict; //������ �����͸� �����ϴ� ��ųʸ�(sequence ����)
    private string nextSceneName = "";

    //���� �������� ������ �� ȣ��. �ΰ��� json���� �̸��� ����
    public void StartDialogue(string dialogueFile, string choiceFile, string nextScene = "")
    {
        currentLineIndex = 0;
        nextSceneName = nextScene;

        choiceDict = new Dictionary<int, ChoiceEntry>(); // �׻� �ʱ�ȭ

        LoadDialogueFromJson(dialogueFile); // ��� �ε�
        LoadChoicesFromJson(choiceFile); // ������ �ε�
        ShowDialogue(); // ù ��� ���
    }

    //��� json ���� �ε�
    private void LoadDialogueFromJson(string fileName)
    {
        TextAsset json = Resources.Load<TextAsset>(fileName); 
        if (json == null)
        {
            Debug.LogError($"JSON ���� �� ã��: {fileName}");
            return;
        }
        dialogueLines = new List<DialogueLine>(JsonHelper.FromJson<DialogueLine>(json.text));
    }

    //������ json���� �ε�(sequence �������� ��ųʸ��� ����)
    private void LoadChoicesFromJson(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;  // ������ ������ ���� ���

        TextAsset json = Resources.Load<TextAsset>(fileName);  // ��ü ��� �Ѱ����Ƿ� �״�� �ޱ�

        if (json == null)
        {
            Debug.LogWarning($" ������ JSON ������ ã�� �� �����ϴ�: {fileName}");
            return;
        }

        ChoiceEntry[] choices = JsonHelper.FromJson<ChoiceEntry>(json.text);
        choiceDict = new Dictionary<int, ChoiceEntry>();
        foreach (var c in choices)
        {
            choiceDict[c.sequence] = c;
        }
    }

    // ���� ��� ���(�������� ������ ChoiceManager ȣ��)
    public void ShowDialogue()
    {
        if (currentLineIndex >= dialogueLines.Count)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log("��� ��, ���������� �̵�: " + nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("��� ��! �̵��� �� ����");
            }
            
            return;
        }
        // �������� �ִ� �����̸� ������ ���� ���
        if (choiceDict.ContainsKey(currentLineIndex))
        {
            ChoiceManager.Instance.ShowChoice(choiceDict[currentLineIndex], OnChoiceResult);
            return;
        }

        DialogueLine line = dialogueLines[currentLineIndex];
        if (line == null) return;

        currentText = line.dialogue_text;
        dialogueText.text = "";
        // �������
        AudioClip clip = line.GetVoice();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        StartCoroutine(TypeSentence(currentText));// Ÿ���ν���
    }

    // Ÿ���� ȿ�� �ڷ�ƾ (�� ���ھ� ���)
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // ȭ���� ��ġ���� �� ȣ���
    public void OnTouch()
    {
        if (isTyping)
        {
            dialogueText.text = currentText; // ��ü ���� ǥ��
            StopAllCoroutines();
            isTyping = false;
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            currentLineIndex++; // ���� ���� �̵�
            ShowDialogue();
        }
    }

    // ������ ��� �ݹ� �Լ� (����/���� ó��)
    private void OnChoiceResult(bool isCorrect)
    {
        if (isCorrect)
        {
            currentLineIndex++; //���� ���� �̵�

            if (currentLineIndex >= dialogueLines.Count)
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    Debug.Log("��� ��, ���� ������ �̵�: " + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("��� ��! �̵��� �� ����");
                }
            }
            else
            {
                ShowDialogue();
            }
        }
        else
        {
            Debug.Log("����! ChoiceManager�� �ǵ�� ó��");
        }
    }
}
