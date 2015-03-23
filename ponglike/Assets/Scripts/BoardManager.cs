using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
  
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject LeftCornerWall;
    public GameObject RightCornerWall;
    public GameObject FogOfWar;

    private Transform boardHolder;
    private Dictionary<string, GameObject> fogOfWarByPosition = new Dictionary<string, GameObject>(); 

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //clear
        foreach (var fogOfWar in fogOfWarByPosition.Values)
        {
            DestroyObject(fogOfWar);
        }
        fogOfWarByPosition.Clear();
        
        //fill
        boardHolder = new GameObject("Board").transform;
        for (var x = 0; x < GameState.Instance.Columns; x++)
        {
            for (var y = 0; y < GameState.Instance.Rows; y++)
            {
                var toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];
                var shouldInstantiateFogOfWar = true;
                if (y == 0 || y == GameState.Instance.Rows - 1)
                {
                    shouldInstantiateFogOfWar = false;
                    if (x == 0)
                        toInstantiate = LeftCornerWall;
                    else if (x == GameState.Instance.Columns - 1)
                        toInstantiate = RightCornerWall;
                    else
                        toInstantiate = WallTiles[Random.Range(0, WallTiles.Length)];
                }

                var position = new Vector3(x, y, 0f);
                var instance = Instantiate(toInstantiate, position, Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

                if (shouldInstantiateFogOfWar)
                {
                    var fogOfWar = Instantiate(FogOfWar, position, Quaternion.identity) as GameObject;
                    fogOfWar.transform.SetParent(boardHolder);
                    fogOfWar.GetComponent<Renderer>().enabled = false;
                    fogOfWarByPosition[position.ToString()] = fogOfWar;
                }
            }
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene()
    {
        BoardSetup();
    }

    public void SetFogOfWarForPosition(Vector3 position, bool shouldEnable)
    {
        fogOfWarByPosition[position.ToString()].GetComponent<Renderer>().enabled = shouldEnable;
    }

    public void SetAllFogOfWar(bool shouldEnable)
    {
        foreach (var fogOfWar in fogOfWarByPosition.Values)
        {
            fogOfWar.GetComponent<Renderer>().enabled = shouldEnable;

        }
    }
}
