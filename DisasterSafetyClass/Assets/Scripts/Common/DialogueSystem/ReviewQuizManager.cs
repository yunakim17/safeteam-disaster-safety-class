using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReviewQuizManager : MonoBehaviour
{
    [Header("UI ���")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;
    public GameObject oImageButton;  // O �̹��� (���� + ��ư)
    public GameObject xImageButton;  // X �̹��� (���� + ��ư)
    public Button nextButton;

    [Header("���� ����")]
    [TextArea] public string quizQuestion;
    [TextArea] public string feedbackMessage;  // ���� �� �ǵ��
    public bool correctIsO;  // true�� O�� ����, false�� X�� ����

    void Start()
    {
        // UI �ʱ�ȭ
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

        // ��ư ��Ȱ��ȭ
        oImageButton.GetComponent<Button>().interactable = false;
        xImageButton.GetComponent<Button>().interactable = false;

        if (isCorrect)
        {
            // ������ ��� �ǵ�� ���� �ٷ� ���� ��ư Ȱ��ȭ
            nextButton.gameObject.SetActive(true);
            feedbackText.gameObject.SetActive(false); // Ȥ�� �𸣴� �����
        }
        else
        {
            // ������ ��� �ǵ�� �����ֱ� + ���� ��ư
            feedbackText.text = feedbackMessage;
            feedbackText.gameObject.SetActive(true);       // �� �� �߿�!!
            nextButton.gameObject.SetActive(true);
        }
    }


    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
