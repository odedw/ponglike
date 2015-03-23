using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public static GameState Instance { get; private set; }

    //Configuration
    public int Columns = 12;
    public int Rows = 9;

    //Game State
    private bool isPlayersTurn = true;
    private bool isEnemyTurn;
    public bool IsPlayersTurn {get { return isPlayersTurn; } set { isPlayersTurn = value; isEnemyTurn = !value; }}
    public bool IsEnemyTurn { get { return isEnemyTurn; } set { isEnemyTurn = value; isPlayersTurn = !value; } }

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
