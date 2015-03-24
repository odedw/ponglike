using UnityEngine;
using System.Collections;

public class RandomBoardPlacer : BoardPlacer {
    public override void PlaceBoard(BoardManager boardManager)
    {
        Debug.Log("Board placer placed board");
    }
}
