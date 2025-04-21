using UnityEngine;

public class Eq_Step3_S1_DialogueStarter : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Shake shakeTarget;  // ��� ��� ������Ʈ ����

    void Start()
    {
        if (shakeTarget != null)
        {
            shakeTarget.StartShake(0.3f);
        }

        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step3/Eq_Step3_S1_dialogues",
            "Dialogues/Eq_Step3/Eq_Step3_S1_choices",
            "Eq_Step3_S2"
        );
    }

    void StopShakeSmoothly()
    {
        if (shakeTarget != null)
        {
            shakeTarget.StopShake(1f); // 1�� ���� ������ ����
        }
    }
}
