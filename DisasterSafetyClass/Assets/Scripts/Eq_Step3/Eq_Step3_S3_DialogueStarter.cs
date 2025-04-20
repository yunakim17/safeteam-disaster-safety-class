using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step3_S3_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("dialogueManager�� ������� �ʾҽ��ϴ�!");
            return;
        }

        Debug.Log("dialogueManager �����, StartDialogue ȣ��");

        dialogueManager.StartDialogue(
            "",
            "",
            "Eq_Step3_S4"
        );
    }
}
