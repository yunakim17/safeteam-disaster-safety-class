using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        var test = Resources.Load<TextAsset>("Dialogues/Eq_Step1/Eq_Step1_S1_dialogues");

        if (test == null)
        {
            Debug.LogError("��¥ �� ã��! ������ ���ų� ��ΰ� Ʋ�Ƚ��ϴ�.");
        }
        else
        {
            Debug.Log("JSON ã��! ����: " + test.text.Substring(0, Mathf.Min(30, test.text.Length)) + "...");
        }
    }
}
