using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            player p = FindObjectOfType<player>();

            p.bulletCount += 3;
            p.bulletText.text = p.bulletCount.ToString();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("stone"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Boss"))
        {
            Boss boss = other.GetComponent<Boss>();

            if (boss != null)
            {
                boss.TakeDamage();
            }

            Destroy(gameObject);
        }
    }
}