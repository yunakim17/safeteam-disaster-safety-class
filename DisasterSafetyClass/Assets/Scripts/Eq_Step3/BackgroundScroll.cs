using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    public MeshRenderer meshRenderer;

    private bool isScrolling = true;

    void Update()
    {
        // ���� ���� �� ��ũ�� �ߴ�
        if (!isScrolling) return;

        // ��� �ڵ� ��ũ��
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }
}
