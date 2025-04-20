using UnityEngine;

public class UIShake : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private float shakeIntensity = 3f;
    private float fadeOutSpeed = 1f;
    private bool isShaking = false;
    private bool isFadingOut = false; // fade-out Àü¿ë flag

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            Vector2 randomOffset = Random.insideUnitCircle * shakeIntensity;
            rectTransform.anchoredPosition = originalPosition + (Vector3)randomOffset;

            if (isFadingOut)
            {
                shakeIntensity = Mathf.MoveTowards(shakeIntensity, 0f, Time.deltaTime * fadeOutSpeed);
                if (shakeIntensity <= 0.01f)
                {
                    isShaking = false;
                    isFadingOut = false;
                    rectTransform.anchoredPosition = originalPosition;
                }
            }
        }
    }

    public void StartShake(float intensity = 30f, float fadeSpeed = 5f)
    {
        shakeIntensity = intensity;
        fadeOutSpeed = fadeSpeed;
        isShaking = true;
        isFadingOut = false; 
    }

    public void StopShake()
    {
        isFadingOut = true; 
    }
}