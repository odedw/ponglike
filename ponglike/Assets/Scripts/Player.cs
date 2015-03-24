using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Opponent
{
    public GameObject HighlightObject;
    private readonly List<Vector3> possibleMoves = new List<Vector3>();
    private readonly List<GameObject> highlights = new List<GameObject>();
    private bool areMovesHighlighted;
    

    //states
    protected override bool IsUnitsTurn { get { return GameState.Instance.IsPlayersTurn; } set {GameState.Instance.IsPlayersTurn = value;} }

    //unit specific config
    protected override int StartColumn { get { return GameState.Instance.Columns - 1; } }
    protected override int UnitAdvanceDirection { get { return - 1; } }
    protected override int EndColumn { get { return 0; } }
    protected override bool DesiredFogOfWarState { get { return true; } }
    protected override bool ShouldAct { get { return IsUnitsTurn && !areMovesHighlighted && !isMoving; } }  
    private bool waitingForInputMovement;

	// Update is called once per frame
    protected override void OpponentUpdate () {
        //highlight moves for this turn
	    ComputePossibleMoves();
	    HighlightPossibleMoves();
	    waitingForInputMovement = true;
	}

    private void HighlightPossibleMoves()
    {
        foreach (var possibleMove in possibleMoves)
        {
            var highlight = Instantiate(HighlightObject);
            highlight.transform.position = possibleMove;
            highlight.GetComponent<Highlight>().OnHighlighterClicked += Player_OnHighlighterClicked;
            highlights.Add(highlight);
            GameManager.Instance.BoardManager.SetFogOfWarForPosition(possibleMove, false);
        }
        areMovesHighlighted = true;
    }
    
    private void ComputePossibleMoves()
    {        
        int startRow, endRow, column;
        if (!InitialUnitPlacingSet)
        {
            startRow = 1;
            endRow = GameState.Instance.Rows - 2;
            column = StartColumn;
        }
        else
        {
            startRow = (int)transform.position.y == 1 ? 1 : ((int)transform.position.y) - 1;
            endRow = (int)transform.position.y == GameState.Instance.Rows - 2 ? GameState.Instance.Rows - 2 : ((int)transform.position.y) + 1;
            column = ((int)transform.position.x) + UnitAdvanceDirection;
        }

        for (var i = startRow; i <= endRow; i++)
        {
            possibleMoves.Add(new Vector3(column, i, 0));
        }

    }

    void Player_OnHighlighterClicked(GameObject highlighter)
    {
        if (!waitingForInputMovement) return;
        waitingForInputMovement = false;

        //clear highlights
        foreach (var highlight in highlights)
        {
            DestroyObject(highlight);
        }

        possibleMoves.Clear();
        areMovesHighlighted = false;

        Move(highlighter.transform.position);
    }

    protected override void PlaceBoardForOpponent()
    {
        Debug.Log("Player placed board");
    }
}
