using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReviewQuizManager : MonoBehaviour
{
    [Header("UI 요소")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;
    public GameObject oImageButton;  // O 이미지 (선택 + 버튼)
    public GameObject xImageButton;  // X 이미지 (선택 + 버튼)
    public Button nextButton;

    [Header("퀴즈 설정")]
    [TextArea] public string quizQuestion;
    [TextArea] public string feedbackMessage;  // 오답 시 피드백
    public bool correctIsO;  // true면 O가 정답, false면 X가 정답

    void Start()
    {
        // UI 초기화
        questionText.text = quizQuestion;
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        oImageButton.SetActive(true);
        xImageButton.SetActive(true);
    }

    public void SelectO()
    {
        CheckAnswer(userChoseO: true);
    }

    public void SelectX()
    {
        CheckAnswer(userChoseO: false);
    }

    private void CheckAnswer(bool userChoseO)
    {
        bool isCorrect = (userChoseO == correctIsO);

        // 버튼 비활성화
        oImageButton.GetComponent<Button>().interactable = false;
        xImageButton.GetComponent<Button>().interactable = false;

        if (isCorrect)
        {
            // 정답일 경우 피드백 없이 바로 다음 버튼 활성화
            nextButton.gameObject.SetActive(true);
            feedbackText.gameObject.SetActive(false); // 혹시 모르니 숨기기
        }
        else
        {
            // 오답일 경우 피드백 보여주기 + 다음 버튼
            feedbackText.text = feedbackMessage;
            feedbackText.gameObject.SetActive(true);       // 이 줄 중요!!
            nextButton.gameObject.SetActive(true);
        }
    }


    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
