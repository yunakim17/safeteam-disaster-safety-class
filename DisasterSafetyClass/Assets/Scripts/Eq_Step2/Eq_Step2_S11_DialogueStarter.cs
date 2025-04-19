using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step2_S11_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S11_dialogues",
            "",
            "" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
