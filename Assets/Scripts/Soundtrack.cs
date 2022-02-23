using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    GameObject[]soundtrackSFX;
    void Start() 
    {
        soundtrackSFX = GameObject.FindGameObjectsWithTag("Soundtrack");
        if(soundtrackSFX.Length!=1)
        {
            Destroy(soundtrackSFX[1]);
        }

    }

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }  
}
