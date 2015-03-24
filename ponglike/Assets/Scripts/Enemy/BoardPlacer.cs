using UnityEngine;
using System.Collections;

public abstract class BoardPlacer : MonoBehaviour
{
    public abstract void PlaceBoard(BoardManager boardManager);
}
