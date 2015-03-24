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
    protected override bool DesiredFogOfWarState { get { return false; } }

    protected override int UnitAdvanceDirection { get { return 1; }}

    //AI
    private MoveCalculator moveCalculator;
    void Start()
    {
        base.Start();
        moveCalculator = GetComponent<MoveCalculator>();
    }
    void Update()
    {
        //If it's not the player's turn, or we're to compute move or we're moving, exit.
        if (!IsUnitsTurn || isMoving || nextMoveScheduled) return;
        
        if (!fogOfWarReset)
        {
            ResetFogOfWar();
        }

        Invoke("NextMove", MoveDelay);
        nextMoveScheduled = true;
    }


    void NextMove()
    {
        var nextMove = moveCalculator.CalculateNextMove(GameManager.Instance.BoardManager, !InitialUnitPlacingSet, StartColumn, UnitAdvanceDirection);
        Move(nextMove);
        nextMoveScheduled = false;
    }
}
