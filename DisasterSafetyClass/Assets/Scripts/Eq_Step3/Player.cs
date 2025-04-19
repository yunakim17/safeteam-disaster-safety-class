using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public Rigidbody2D PlayerRigidBody;
    private bool isGrounded = true;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    public DistanceBar distanceBar;

    private bool isGameEnded = false;

    public Animator PlayerAnimator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ���� ���� �� ���� x
        if (isGameEnded) return;

        bool isTouch = false;

        // ȭ�� ��ġ ���� (����Ͽ�)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            isTouch = true;

        // ���콺 Ŭ�� ���� (��ǻ�� �׽�Ʈ��)
        if (Input.GetMouseButtonDown(0))
            isTouch = true;

        // �÷��̾� ����
        if (isTouch && isGrounded)
        {
            PlayerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (!isGrounded)
                PlayerAnimator.SetInteger("state", 2);

            isGrounded = true;
        }
    }

    // ��ֹ� �浹 ��
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "obstacle" && !isInvincible)
        {
            // ���� �� �� ����
            if (distanceBar != null)
                distanceBar.ChangeFillColor(distanceBar.hitColor);

            // �ӵ� ����
            if (distanceBar != null)
                distanceBar.timeScale = 0.1f;

            StartCoroutine(InvincibleRoutine());
        }
    }

    // ��ֹ� �浹 �� ������ + ���� ���� ����
    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        float blinkTime = 0.1f;
        int blinkCount = 10;

        for (int i = 0; i < blinkCount; i++)
        {
            // ���� ����
            Color c = spriteRenderer.color;
            c.a = 0.3f;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(blinkTime);

            // ���� �ٽ� ����
            c.a = 1f;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(blinkTime);
        }

        isInvincible = false;

        // ���� �ӵ�&�����̴� ���� ����
        if (distanceBar != null)
        {
            distanceBar.timeScale = 1f;
            distanceBar.ChangeFillColor(distanceBar.normalColor);
        }
    }

    public void StopPlayerControl()
    {
        isGameEnded = true;
    }
}
