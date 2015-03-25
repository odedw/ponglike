using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : Opponent
{
    public float MoveDelay = 5f;

    //states
    protected override bool IsUnitsTurn { get { return GameState.Instance.IsEnemyTurn; } set { GameState.Instance.IsEnemyTurn = value; } }
    private bool nextMoveScheduled;
    
    //config
    protected override int StartColumn { get { return 0; }}
    protected override int EndColumn { get { return GameState.Instance.Columns - 1; } }
    protected override bool DesiredFogOfWarState { get { return false; } }
    protected override bool ShouldAct { get { return IsUnitsTurn && !isMoving && !nextMoveScheduled; } }
    protected override int UnitAdvanceDirection { get { return 1; }}

    //AI
    private MoveCalculator moveCalculator;
    private BoardPlacer boardPlacer;

    protected override void Start()
    {
        base.Start();
        moveCalculator = GetComponent<MoveCalculator>();
        boardPlacer = GetComponent<BoardPlacer>();
    }

    protected override void PlaceBoardForOpponent()
    {
        var spent = boardPlacer.PlaceBoard(GameManager.Instance.BoardManager, Gold);
        Gold -= spent;
    }

    protected override void OpponentUpdate()
    {
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
