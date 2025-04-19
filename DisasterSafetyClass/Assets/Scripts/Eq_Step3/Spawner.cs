using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minSpawnDelay;
    public float maxSpawnDelay;
    public GameObject[] gameObjects;

    private bool isSpawning = true;

    void Start()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    // ������Ʈ ���� ����
    void Spawn()
    {
        // ���� ���� �� ���� �ߴ�
        if (!isSpawning) return;

        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("Spawn");
    }
}
