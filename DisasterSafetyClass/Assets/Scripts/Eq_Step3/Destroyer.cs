using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void Update()
    {
        // ��ֹ��� ȭ�� ������ �Ѿ�� ����
        if (transform.position.x < -12)
        {
            Destroy(gameObject);
        }
    }
}
