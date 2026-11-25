using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player p = other.GetComponent<player>();

            p.bulletCount++;

            p.bulletText.text = p.bulletCount.ToString();

            Destroy(gameObject);
        }
    }
}
