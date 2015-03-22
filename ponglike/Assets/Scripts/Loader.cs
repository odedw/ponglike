using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject GameStateObject;
    public GameObject GameManagerObject;
//    public GameObject SoundManager;

    void Awake()
    {
        if (GameState.Instance == null)
        {
            Instantiate(GameStateObject);
        }

        if (GameManager.Instance == null)
        {
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
