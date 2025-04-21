using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    // InputField
    public TMP_InputField schoolInput;
    public TMP_InputField nicknameInput;

    // Dropdown
    public TMP_Dropdown ageDropdown;

    // Image
    public Image schoolLabel;
    public Image ageLabel;
    public Image nicknameLabel;
    public Image ageLabel2;

    // Button
    public Button schoolButton;
    public Button kindergartenButton;
    public Button checkButton;
    public Button startButton;

    // Warning Text
    public GameObject schoolWarning;
    public GameObject nicknameWarning;

    // �Է°� ����
    public void OnClickStartButton()
    {
        bool isValid = true;

        // �б� �Է� Ȯ��
        if (string.IsNullOrWhiteSpace(schoolInput.text))
        {
            schoolWarning.SetActive(true);
            isValid = false;
        }
        else
        {
            schoolWarning.SetActive(false);
        }

        // �г��� �Է� Ȯ��
        if (string.IsNullOrWhiteSpace(nicknameInput.text))
        {
            nicknameWarning.SetActive(true);
            isValid = false;
        }
        else
        {
            nicknameWarning.SetActive(false);
        }

        if (isValid)
        {
            this.GetComponent<SceneChanger>().Main(); // �� ��ȯ
        }
    }
}
