using UnityEngine;

public class Eq_Step2_S8_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S8_dialogues",
            "Dialogues/Eq_Step2/Eq_Step2_S8_choices",
            "Eq_Step2_S9" // ���� �� �̸�
        );
    }
}
