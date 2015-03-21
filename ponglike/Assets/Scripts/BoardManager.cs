using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    public int Columns = 12;
    public int Rows = 9;
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject LeftCornerWall;
    public GameObject RightCornerWall;

    private Transform boardHolder;

    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (var x = 0; x < Columns; x++)
        {
            for (var y = 0; y < Rows; y++)
            {
                var toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];

                if (y == 0 || y == Rows-1)
                {
                    if (x == 0)
                        toInstantiate = LeftCornerWall;
                    else if (x == Columns - 1)
                        toInstantiate = RightCornerWall;
                    else
                        toInstantiate = WallTiles[Random.Range(0, WallTiles.Length)];

                    Debug.Log(y+","+x+": "+toInstantiate.name);

                }

                var instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene()
    {
        BoardSetup();
    }
}
