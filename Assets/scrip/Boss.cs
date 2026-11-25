using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 originalScale;
    private int direction = 1;
    public int health = 10;
    private bool isHit = false;
    private SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    public Slider healthBar;

    private player playerScript;
    private bool chasing = false;
    private float chaseTimer = 0f;

    private bool patrolMode = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        FaceDirection();
        healthBar.maxValue = health;
        healthBar.value = health;

        playerScript = FindObjectOfType<player>();
        
    }
    void FixedUpdate()
    {
        if (patrolMode)
        {
            // گشت‌زنی
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            if (!playerScript.onSafePlatform)
            {
                chaseTimer += Time.fixedDeltaTime;

                if (chaseTimer >= 2f)
                {
                    patrolMode = false;
                    chasing = true;
                }
            }
            else
            {
                chaseTimer = 0f;
            }
        }
        else
        {
            // تعقیب بازیکن
            direction = playerScript.transform.position.x > transform.position.x ? 1 : -1;

            FaceDirection();

            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            // اگر بازیکن دوباره روی سکو رفت
            if (playerScript.onSafePlatform)
            {
                patrolMode = true;
                chasing = false;
                chaseTimer = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("stone"))
        {
            direction *= -1;
            FaceDirection();

            if (chasing)
            {
                chasing = false;
                patrolMode = true;
                chaseTimer = 0f;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            player p = collision.gameObject.GetComponent<player>();

            if (p != null)
            {
                p.GameOver();
            }
        }
    }


    void FaceDirection()
    {
        transform.localScale = new Vector3(
            direction > 0 ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x),
            originalScale.y,
            originalScale.z
        );
    }


    public void TakeDamage()
    {
        if (isHit)
            return;

        health--;
        healthBar.value = health;

        StartCoroutine(HitEffect());

        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator HitEffect()
    {
        isHit = true;

        sr.enabled = false;
        yield return new WaitForSeconds(0.15f);

        sr.enabled = true;
        yield return new WaitForSeconds(0.15f);

        sr.enabled = false;
        yield return new WaitForSeconds(0.15f);

        sr.enabled = true;

        isHit = false;
    }

    void Die()
    {
        rb.velocity = Vector2.zero;

        GetComponent<Collider2D>().enabled = false;

        rb.freezeRotation = false;

        Destroy(gameObject, 3f);
    }
}
