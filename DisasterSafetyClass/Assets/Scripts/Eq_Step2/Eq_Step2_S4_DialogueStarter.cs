using UnityEngine;

public class Eq_Step2_S4_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S4_dialogues",
            "Dialogues/Eq_Step2/Eq_Step2_S4_choices",
            "Eq_Step2_S5" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
