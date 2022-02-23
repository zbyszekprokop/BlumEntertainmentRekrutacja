using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource pickCoin;
    int coinValue = 1;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CountCoin.coins.ChangeScore(coinValue);
            pickCoin.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            gameObject.GetComponent<Collider2D>().enabled=false;
            Destroy(gameObject, 0.6f);
        }
    }
}
