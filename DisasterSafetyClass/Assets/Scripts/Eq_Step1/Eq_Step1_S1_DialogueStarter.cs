using UnityEngine;

public class Eq_Step1_S1_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step1/Eq_Step1_S1_dialogues",
            "",
            "Eq_Step1_S2");
    }
}
