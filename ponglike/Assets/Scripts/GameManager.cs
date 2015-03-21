using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance { get; private set; }

    private BoardManager boardManager;
    private bool settingUp = true;	

    private void Awake()
    {
        //Singleton stuff
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //get components
        boardManager = GetComponent<BoardManager>();

        InitGame();
    }

    private void InitGame()
    {
        settingUp = true;
        boardManager.SetupScene();
        settingUp = false;
    }
}
