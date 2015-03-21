﻿using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject GameManagerObject;
//    public GameObject SoundManager;

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.Instance == null)
        {
            //Instantiate gameManager prefab
            Instantiate(GameManagerObject);
        }

        //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
//        if (SoundManager.instance == null)
//        {

            //Instantiate SoundManager prefab
//            Instantiate(soundManager);
//        }
    }
}
