using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool stopEnemy = false;
    public float speed = 2f;
    private int direction = -1;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (stopEnemy)
            return;

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // آیا بازیکن از بالا برخورد کرده؟
            if (collision.contacts[0].normal.y < -0.5f)
            {
                player p = collision.gameObject.GetComponent<player>();

                p.bulletCount += 3;
                p.bulletText.text = p.bulletCount.ToString();
                // دشمن نابود شود
                Destroy(gameObject);

                // بازیکن کمی به بالا بپرد
                collision.gameObject.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(
                        collision.gameObject.GetComponent<Rigidbody2D>().velocity.x,
                        8f
                    );
            }
            else
            {
                stopEnemy = true;
                rb.velocity = Vector2.zero;
                // برخورد از بغل = باخت
                collision.gameObject.GetComponent<player>().GameOver();
            }
        }

        if (collision.gameObject.CompareTag("stone"))
        {
            direction *= -1;

            transform.localScale = new Vector3(
                direction > 0 ? -1 : 1,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}