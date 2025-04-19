using UnityEngine;

public class Eq_Step2_S2_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        if (dialogueManager == null)
        {
            Debug.LogError("dialogueManager�� ������� �ʾҽ��ϴ�!");
            return;
        }

        Debug.Log("dialogueManager �����, StartDialogue ȣ��");
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S2_dialogues",
            "Dialogues/Eq_Step2/Eq_Step2_S2_choices",
            "Eq_Step2_S3"
        );
    }
}