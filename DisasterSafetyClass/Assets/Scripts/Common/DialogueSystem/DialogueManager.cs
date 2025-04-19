using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 자막 텍스트 표시용
    public AudioSource audioSource; // 음성 재생용
    public float typingSpeed = 0.05f; // 타이핑 속도

    private bool isTyping = false; //현재 타이핑 중 여부
    private string currentText = ""; // 현재 출력중인 대사 텍스트
    private int currentLineIndex = 0; // 현재 몇 번째 대사인지
    private List<DialogueLine> dialogueLines; //대사리스트
    private Dictionary<int, ChoiceEntry> choiceDict; //선택지 데이터를 저장하는 딕셔너리(sequence 기준)
    private string nextSceneName = "";

    //대사와 선택지를 시작할 때 호출. 두개의 json파일 이름을 받음
    public void StartDialogue(string dialogueFile, string choiceFile, string nextScene = "")
    {
        currentLineIndex = 0;
        nextSceneName = nextScene;

        choiceDict = new Dictionary<int, ChoiceEntry>(); // 항상 초기화

        LoadDialogueFromJson(dialogueFile); // 대사 로드
        LoadChoicesFromJson(choiceFile); // 선택지 로드
        ShowDialogue(); // 첫 대사 출력
    }

    //대사 json 파일 로드
    private void LoadDialogueFromJson(string fileName)
    {
        TextAsset json = Resources.Load<TextAsset>(fileName); 
        if (json == null)
        {
            Debug.LogError($"JSON 파일 못 찾음: {fileName}");
            return;
        }
        dialogueLines = new List<DialogueLine>(JsonHelper.FromJson<DialogueLine>(json.text));
    }

    //선택지 json파일 로드(sequence 기준으로 딕셔너리에 저장)
    private void LoadChoicesFromJson(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;  // 선택지 파일이 없는 경우

        TextAsset json = Resources.Load<TextAsset>(fileName);  // 전체 경로 넘겼으므로 그대로 받기

        if (json == null)
        {
            Debug.LogWarning($" 선택지 JSON 파일을 찾을 수 없습니다: {fileName}");
            return;
        }

        ChoiceEntry[] choices = JsonHelper.FromJson<ChoiceEntry>(json.text);
        choiceDict = new Dictionary<int, ChoiceEntry>();
        foreach (var c in choices)
        {
            choiceDict[c.sequence] = c;
        }
    }

    // 현재 대사 출력(선택지가 있으면 ChoiceManager 호출)
    public void ShowDialogue()
    {
        if (currentLineIndex >= dialogueLines.Count)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log("대사 끝, 다음씬으로 이동: " + nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("대사 끝! 이동할 씬 없음");
            }
            
            return;
        }
        // 선택지가 있는 시점이면 선택지 먼저 출력
        if (choiceDict.ContainsKey(currentLineIndex))
        {
            ChoiceManager.Instance.ShowChoice(choiceDict[currentLineIndex], OnChoiceResult);
            return;
        }

        DialogueLine line = dialogueLines[currentLineIndex];
        if (line == null) return;

        currentText = line.dialogue_text;
        dialogueText.text = "";
        // 음성재생
        AudioClip clip = line.GetVoice();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        StartCoroutine(TypeSentence(currentText));// 타이핑시작
    }

    // 타이핑 효과 코루틴 (한 글자씩 출력)
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

    // 화면을 터치했을 때 호출됨
    public void OnTouch()
    {
        if (isTyping)
        {
            dialogueText.text = currentText; // 전체 문장 표시
            StopAllCoroutines();
            isTyping = false;
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            currentLineIndex++; // 다음 대사로 이동
            ShowDialogue();
        }
    }

    // 선택지 결과 콜백 함수 (정답/오답 처리)
    private void OnChoiceResult(bool isCorrect)
    {
        if (isCorrect)
        {
            currentLineIndex++; //다음 대사로 이동

            if (currentLineIndex >= dialogueLines.Count)
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    Debug.Log("대사 끝, 다음 씬으로 이동: " + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("대사 끝! 이동할 씬 없음");
                }
            }
            else
            {
                ShowDialogue();
            }
        }
        else
        {
            Debug.Log("오답! ChoiceManager가 피드백 처리");
        }
    }
}
