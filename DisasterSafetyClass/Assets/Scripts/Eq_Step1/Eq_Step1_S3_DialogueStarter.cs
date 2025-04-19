using UnityEngine;

public class Eq_Step1_S3_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step1/Eq_Step1_S3_dialogues",
            "", 
            "Eq_Step1_S4");
    }
}
