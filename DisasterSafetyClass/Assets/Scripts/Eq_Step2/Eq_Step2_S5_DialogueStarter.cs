using UnityEngine;

public class Eq_Step2_S5_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S5_dialogues",
            "",
            "Eq_Step2_S6" // ���� �� �̸�
        );
    }
}
