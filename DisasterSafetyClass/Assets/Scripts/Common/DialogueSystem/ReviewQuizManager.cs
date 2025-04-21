using System.Collections;
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
    [TextArea] public string correctFeedbackMessage; // ���� �� �ǵ��
    [TextArea] public string wrongFeedbackMessage;   // ���� �� �ǵ��
    public bool correctIsO; // true�� O�� ����, false�� X�� ����

    [Header("ĳ���� �̹���")]
    public Image characterImage;          // ĳ���� �̹��� (mong_speak_0 ������Ʈ)
    public Sprite defaultSprite;          // �⺻ ���� �̹���
    public Sprite correctAnswerSprite;    // ���� ������ �� �̹��� (mong)

    void Start()
    {
        // UI �ʱ�ȭ
        questionText.text = quizQuestion;
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        oImageButton.SetActive(true);
        xImageButton.SetActive(true);

        // ĳ���� �ʱ� �̹��� ����
        if (characterImage != null && defaultSprite != null)
        {
            characterImage.sprite = defaultSprite;
        }
    }

    public void SelectO()
    {
        StartCoroutine(PressEffect(oImageButton));
        CheckAnswer(userChoseO: true);
    }

    public void SelectX()
    {
        StartCoroutine(PressEffect(xImageButton));
        CheckAnswer(userChoseO: false);
    }

    private void CheckAnswer(bool userChoseO)
    {
        bool isCorrect = (userChoseO == correctIsO);

        // ��ư ��Ȱ��ȭ
        oImageButton.GetComponent<Button>().interactable = false;
        xImageButton.GetComponent<Button>().interactable = false;

        // �ǵ�� ǥ��
        if (isCorrect)
        {
            feedbackText.text = correctFeedbackMessage;
            feedbackText.color = Color.green;

            // ĳ���� �̹��� ����
            if (characterImage != null && correctAnswerSprite != null)
            {
                characterImage.sprite = correctAnswerSprite;
            }
        }
        else
        {
            feedbackText.text = wrongFeedbackMessage;
            feedbackText.color = Color.red;

            // �����̸� �⺻ �̹��� ���� or �ǵ�����
            if (characterImage != null && defaultSprite != null)
            {
                characterImage.sprite = defaultSprite;
            }
        }

        feedbackText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // ��ư ���� ȿ��
    IEnumerator PressEffect(GameObject buttonObj)
    {
        Vector3 originalScale = buttonObj.transform.localScale;
        buttonObj.transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        buttonObj.transform.localScale = originalScale;
    }
}
