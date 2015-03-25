using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    public static GameManager Instance { get; private set; }

    public BoardManager BoardManager { get; set; }
    public Store Store { get; set; }

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
        BoardManager = GetComponent<BoardManager>();
        Store = GetComponent<Store>();

        InitGame();
    }

    private void InitGame()
    {
        settingUp = true;
        BoardManager.SetupScene();
        settingUp = false;
    }
}
