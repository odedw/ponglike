using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : MovingObject
{
    public float MoveDelay = 5f;

    //states
    protected override bool IsUnitsTurn { get { return GameState.Instance.IsEnemyTurn; } set { GameState.Instance.IsEnemyTurn = value; } }
    private bool nextMoveScheduled;
    //config
    protected override int StartColumn { get { return 0; }}
    protected override int EndColumn { get { return GameState.Instance.Columns - 1; } }
    protected override int UnitAdvanceDirection { get { return 1; }}

    void Update()
    {
        //If it's not the player's turn, or we're to compute move or we're moving, exit.
        if (!IsUnitsTurn || isMoving || nextMoveScheduled) return;

        Invoke("NextMove", MoveDelay);
        nextMoveScheduled = true;
    }


    void NextMove()
    {
        var nextMove = ComputeNextMove();
        Move(nextMove);
        nextMoveScheduled = false;
    }

    Vector2 ComputeNextMove()
    {
        if (!InitialUnitPlacingSet)
        {
            return new Vector2(StartColumn, Random.Range(1, GameState.Instance.Rows - 2));
        }

        return new Vector2(transform.position.x + UnitAdvanceDirection,
            Mathf.Clamp(transform.position.y + Random.Range(-1, 2), 2, GameState.Instance.Columns - 2));
    }
}
