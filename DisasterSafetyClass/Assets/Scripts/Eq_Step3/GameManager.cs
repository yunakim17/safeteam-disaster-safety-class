using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject introUI;
    public Button startButton;

    public GameObject obstacleSpawner;
    public GameObject distanceBar;

    public GameObject clearUI;

    void Start()
    {
        introUI.SetActive(true);

        obstacleSpawner.SetActive(false);
        distanceBar.SetActive(false);

        startButton.onClick.AddListener(StartGame);
    }

    /* ���� ���� ��
     * - ��Ʈ�� UI ��Ȱ��ȭ
     * - ��ֹ� ������ & �Ÿ� �� Ȱ��ȭ
     */
    void StartGame()
    {
        introUI.SetActive(false);

        obstacleSpawner.SetActive(true);
        distanceBar.SetActive(true);
    }

    // ���� ���� �� Ŭ���� UI Ȱ��ȭ
    public void ShowClearText()
    {
        clearUI.SetActive(true);
    }
}