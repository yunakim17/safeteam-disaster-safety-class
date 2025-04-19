using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step2_S9_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S9_dialogues",
            "",
            "Eq_Step2_S10" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
