using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step3_S4_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("dialogueManager가 연결되지 않았습니다!");
            return;
        }

        Debug.Log("dialogueManager 연결됨, StartDialogue 호출");

        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step3/Eq_Step3_S4_dialogues",
            "Dialogues/Eq_Step3/Eq_Step3_S4_choices",
            ""
        );
    }
}
