using UnityEngine;
using System.Collections;


public abstract class BoardPlacer : MonoBehaviour
{
    public abstract int PlaceBoard(BoardManager boardManager, int gold);
}
