using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;

    private GameObject currentBullet;

    public float spawnTime = 5f;

    float timer;

    void Start()
    {
        timer = spawnTime;
    }

    void Update()
    {
        if (currentBullet == null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                currentBullet =
                    Instantiate(
                        bulletPrefab,
                        transform.position,
                        Quaternion.identity);

                timer = spawnTime;
            }
        }
    }
}
