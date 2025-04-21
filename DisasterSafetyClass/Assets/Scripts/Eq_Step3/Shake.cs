using UnityEngine;

public class Shake : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeIntensity = 0f;
    private float fadeOutSpeed = 1f;
    private bool isShaking = false;
    private bool isFading = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            Vector3 offset = Random.insideUnitSphere * shakeIntensity;
            offset.z = 0f; // 2D���� Z�� ��鸲 ����
            transform.localPosition = originalPosition + offset;

            if (isFading)
            {
                shakeIntensity = Mathf.MoveTowards(shakeIntensity, 0f, fadeOutSpeed * Time.deltaTime);
                if (shakeIntensity <= 0.01f)
                {
                    isShaking = false;
                    transform.localPosition = originalPosition;
                }
            }
        }
    }

    // ���� ����
    public void StartShake(float intensity)
    {
        originalPosition = transform.localPosition;
        shakeIntensity = intensity;
        isShaking = true;
        isFading = false;
    }

    // �ε巴�� ���߱�
    public void StopShake(float fadeTime = 1f)
    {
        fadeOutSpeed = fadeTime > 0f ? 1f / fadeTime : 999f;
        isFading = true;
    }
}
