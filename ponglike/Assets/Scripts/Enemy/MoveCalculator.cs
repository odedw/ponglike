using UnityEngine;
using System.Collections;

public abstract class MoveCalculator : MonoBehaviour {
    public abstract Vector2 CalculateNextMove(BoardManager boardManager, bool isFirstMove, int startColumn, int unitAdvanceDirection);
}
