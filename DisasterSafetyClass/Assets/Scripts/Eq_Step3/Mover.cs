using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 1f;

    void Update()
    {
        // ��ֹ� �������� �̵�
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    // ���� ���� �� ��� ��ֹ� ����
    public void RemoveSelf()
    {
        Destroy(gameObject);
    }
}
