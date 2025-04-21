using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolType : MonoBehaviour
{
    public Button schoolButton;
    public Button kindergartenButton;

    public Color32 selectedColor = new Color32(255, 130, 130, 255);
    public Color32 defaultColor = new Color32(249, 221, 105, 255);

    public string selectedSchoolType = "�ʵ��б�"; // ������ �б� Ÿ�� ���� ���� -> ���Ŀ� DB ���� �� ���

    void Start()
    {
        SetSchoolType("�ʵ��б�");
    }

    public void OnClickSchool()
    {
        SetSchoolType("�ʵ��б�");
    }

    public void OnClickKindergarten()
    {
        SetSchoolType("��ġ��");
    }

    // ���õ� ��ư�� ���� ����
    void SetSchoolType(string type)
    {
        selectedSchoolType = type;

        if (type == "�ʵ��б�")
        {
            schoolButton.image.color = selectedColor;
            kindergartenButton.image.color = defaultColor;
        }
        else
        {
            kindergartenButton.image.color = selectedColor;
            schoolButton.image.color = defaultColor;
        }
    }
}
