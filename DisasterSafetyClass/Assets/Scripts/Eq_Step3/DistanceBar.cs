using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBar : MonoBehaviour
{
    public Slider distanceSlider;
    public float totalDuration = 15f;
    public float timeScale = 1f;

    private float elapsedTime = 0f;
    private bool isEndingTriggered = false;

    public Color normalColor = new Color32(126, 200, 227, 255);
    public Color hitColor = new Color32(255, 69, 0, 255);
    private Image fillImage;

    [SerializeField] private GameObject schoolHallway;
    [SerializeField] private GameObject playground;

    [SerializeField] private Spawner obstacleSpawnerScript;
    [SerializeField] private BackgroundScroll backgroundScrollScript;
    [SerializeField] private Player playerScript;

    void Start()
    {
        // �����̴� ���� ����
        fillImage = distanceSlider.fillRect.GetComponent<Image>();
        fillImage.color = normalColor;
    }

    void Update()
    {
        if (!isEndingTriggered)
        {
            // ���� �ð��� ���� �����̴� ����
            if (elapsedTime < totalDuration)
            {
                elapsedTime += Time.deltaTime * timeScale;
                float progress = elapsedTime / totalDuration;
                distanceSlider.value = progress;
            }
            else
                // �����̴��� �� ���� ���� ����
                TriggerEndingSequence();
        }  
    }

    // �����̴� ���� ����
    public void ChangeFillColor(Color newColor)
    {
        fillImage.color = newColor;
    }

    // �����̴��� �� ���� ���� ����
    private void TriggerEndingSequence()
    {
        isEndingTriggered = true;

        // ��ֹ� ���� �ߴ�
        if (obstacleSpawnerScript != null)
            obstacleSpawnerScript.StopSpawning();

        // ��� ��ȯ
        if (schoolHallway != null)
            schoolHallway.SetActive(false);

        if (playground != null)
            playground.SetActive(true);

        // ��� ��ũ�� �ߴ�
        if (backgroundScrollScript != null)
            backgroundScrollScript.StopScrolling();

        // �����ִ� ��ֹ� ����
        Mover[] movers = FindObjectsOfType<Mover>();
        foreach (Mover mover in movers)
        {
            mover.RemoveSelf();
        }

        // �÷��̾� ���� x
        if (playerScript != null)
            playerScript.StopPlayerControl();
    }
}
