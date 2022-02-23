using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountCoin : MonoBehaviour
{
    public TextMeshProUGUI UIText;
    public static CountCoin coins;
    int score;

    void Start() 
    {
        if(coins == null)
        {
            coins = this;
        }
    }
    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        UIText.text = score.ToString();
    }

}
