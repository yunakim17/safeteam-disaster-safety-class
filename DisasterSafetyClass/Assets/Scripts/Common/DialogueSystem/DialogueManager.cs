using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // �ڸ� �ؽ�Ʈ ǥ�ÿ�
    public AudioSource audioSource; // ���� �����
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�
    public GameObject nextButton; //���� �� �̵� ��ư

    public Button btn1_2;
    public Button btn3_4;
    public Button btn5_6;
    public Button btn7;

    public UIShake uiShake;

    private bool isTyping = false; //���� Ÿ���� �� ����
    private string currentText = ""; // ���� ������� ��� �ؽ�Ʈ
    private int currentLineIndex = 0; // ���� �� ��° �������
    private List<DialogueLine> dialogueLines; //��縮��Ʈ
    private Dictionary<int, ChoiceEntry> choiceDict; //������ �����͸� �����ϴ� ��ųʸ�(sequence ����)
    private string nextSceneName = "";

    private HashSet<int> visitedIndices = new HashSet<int>(); //��ư�� ��� �� ���� ������

    //���� �������� ������ �� ȣ��. �ΰ��� json���� �̸��� ����
    public void StartDialogue(string dialogueFile, string choiceFile, string nextScene = "")
    {
        currentLineIndex = 0;
        nextSceneName = nextScene;

        choiceDict = new Dictionary<int, ChoiceEntry>(); // �׻� �ʱ�ȭ

        LoadDialogueFromJson(dialogueFile); // ��� �ε�
        LoadChoicesFromJson(choiceFile); // ������ �ε�
        ShowDialogue(); // ù ��� ���

        if (SceneManager.GetActiveScene().name == "Eq_Step1_S3")
        {
            btn1_2.onClick.AddListener(() => ShowSingleLine(1));
            btn3_4.onClick.AddListener(() => ShowSingleLine(2));
            btn5_6.onClick.AddListener(() => ShowSingleLine(3));
            btn7.onClick.AddListener(() => ShowSingleLine(4));
        }

        if (SceneManager.GetActiveScene().name == "Eq_Step3_S1")
        {
            if (uiShake != null)
            {
                Debug.Log("UI ��鸲 ����");
                uiShake.StartShake(2f, 10f);
            }
        }
       
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
        Debug.Log($" ������ {choices.Length}�� �ε��");
        choiceDict = new Dictionary<int, ChoiceEntry>();
        foreach (var c in choices)
        {
            choiceDict[c.sequence] = c;
        }
    }

    // ���� ��� ���(�������� ������ ChoiceManager ȣ��)
    public void ShowDialogue()
    {
        Debug.Log($" ���� ��� �ε���: {currentLineIndex}");
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
        DialogueLine line = dialogueLines[currentLineIndex];
        int currentSequence = line.sequence;

        Debug.Log($"���� ��� �ε���: {currentLineIndex}, ������: {currentSequence}");
        // �������� �ִ� �����̸� ������ ���� ���
        if (choiceDict != null && choiceDict.ContainsKey(currentSequence))
        {
            Debug.Log(" ������ ǥ�� ���� ����!(������ ���)");
            ChoiceManager.Instance.ShowChoice(choiceDict[currentSequence], OnChoiceResult);
            return;
        }

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

        if (SceneManager.GetActiveScene().name == "Eq_Step3_S1" && line.sequence == 3 && uiShake != null)
        {
            Debug.Log("UI ��鸲 ���� ����");
            uiShake.StopShake();
        }
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
        if (ChoiceManager.Instance != null && ChoiceManager.Instance.choicePanel.activeSelf)
        {
            Debug.Log("������ �г��� Ȱ��ȭ�� ���¿����� ��ġ�� ��� �ѱ�� �Ұ�");
            return;
        }
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

    //S3 Ư����� �ε���
    public void ShowSingleLine(int index)
    {
        if (index < 0 || index >= dialogueLines.Count)
        {
            Debug.LogWarning("�߸��� ��� �ε���!");
            return;
        }

        DialogueLine line = dialogueLines[index];
        currentText = line.dialogue_text;
        dialogueText.text = "";

        if (audioSource.isPlaying)
            audioSource.Stop();

        AudioClip clip = line.GetVoice();
        if(clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentText));

        visitedIndices.Add(index);

        if (visitedIndices.Contains(1) && visitedIndices.Contains(2) && visitedIndices.Contains(3) && visitedIndices.Contains(4))
        {
            nextButton.SetActive(true);
        }

    }

    public void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
