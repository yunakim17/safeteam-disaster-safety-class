using UnityEngine;

public class Eq_Step2_S7_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S7_dialogues",
            "",
            "Eq_Step2_S8" // ���� �� �̸�
        );
    }
}
