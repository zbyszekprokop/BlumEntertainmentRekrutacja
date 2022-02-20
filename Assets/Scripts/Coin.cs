using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int coinValue = 1;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CountCoin.coins.ChangeScore(coinValue);
            Debug.Log("colliding");
            Destroy(gameObject);
        }

    }
}
