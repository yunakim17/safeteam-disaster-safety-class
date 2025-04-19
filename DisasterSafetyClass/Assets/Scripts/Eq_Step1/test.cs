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
            Debug.LogError("진짜 못 찾음! 파일이 없거나 경로가 틀렸습니다.");
        }
        else
        {
            Debug.Log("JSON 찾음! 내용: " + test.text.Substring(0, Mathf.Min(30, test.text.Length)) + "...");
        }
    }
}
