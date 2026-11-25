using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    private player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            player.coins++;
            Destroy(gameObject);
        }
    
    }
}
