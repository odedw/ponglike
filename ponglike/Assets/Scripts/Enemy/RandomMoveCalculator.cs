using UnityEngine;
using System.Collections;

public class RandomMoveCalculator : MoveCalculator {

    public override Vector2 CalculateNextMove(BoardManager boardManager, bool isFirstMove, int startColumn, int unitAdvanceDirection)
    {
        if (isFirstMove)
        {
            return new Vector2(startColumn, Random.Range(1, GameState.Instance.Rows - 2));
        }

        return new Vector2(transform.position.x + unitAdvanceDirection,
            Mathf.Clamp(transform.position.y + Random.Range(-1, 2), 2, GameState.Instance.Columns - 2));
    }
}
