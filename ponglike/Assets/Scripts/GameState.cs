using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static GameState Instance { get; private set; }

    //Configuration
    public int Columns = 12;
    public int Rows = 9;

    //Game State
    public bool IsPlayersTurn = true;

    private void Awake()
    {
        //Singleton stuff
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
