using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovingObject
{
    public GameObject HighlightObject;
    private Renderer objectRenderer;
    private readonly List<Vector3> possibleMoves = new List<Vector3>();
    private readonly List<GameObject> highlights = new List<GameObject>();
    private bool areMovesHighlighted;
    

    //states
    protected override bool InitialUnitPlacingSet { get { return objectRenderer.enabled; } set {objectRenderer.enabled = value; } }
    protected override bool IsUnitsTurn { get { return GameState.Instance.IsPlayersTurn; } set {GameState.Instance.IsPlayersTurn = value;} }

    //unit specific config
    protected override int PlacementColumn { get { return GameState.Instance.Columns - 1; } }
    protected override int UnitAdvanceDirection { get { return - 1; } }
    protected override int TargetColumn { get { return 0; } }

    private bool waitingForInputMovement;

	// Use this for initialization
    protected override void Start ()
	{
	    base.Start();
        objectRenderer = GetComponent<Renderer>();
	    InitialUnitPlacingSet = false;
	}
	
	// Update is called once per frame
	void Update () {
        //If it's not the player's turn, or we're waiting for input or we're moving, exit.
        if (!IsUnitsTurn || areMovesHighlighted || isMoving) return;

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
            column = PlacementColumn;
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
}
