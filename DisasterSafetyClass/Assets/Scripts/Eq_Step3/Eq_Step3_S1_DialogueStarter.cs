using UnityEngine;

public class Eq_Step3_S1_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        Debug.Log("dialogueManager ø¨∞·µ , StartDialogue »£√‚");

        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step3/Eq_Step3_S1_dialogues",
            "Dialogues/Eq_Step3/Eq_Step3_S1_choices",
            "Eq_Step3_S2"
        );
    }
}
