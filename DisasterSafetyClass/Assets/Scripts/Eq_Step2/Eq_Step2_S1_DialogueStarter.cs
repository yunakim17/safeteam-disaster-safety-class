using UnityEngine;

public class Eq_Step2_S1_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S1_dialogues",
            "",
            "Eq_Step2_S2" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
