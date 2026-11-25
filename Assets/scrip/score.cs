using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class score : MonoBehaviour
{
    private player player;

    TextMeshProUGUI coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = player.coins.ToString();
        
    }
}
