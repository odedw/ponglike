using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovingObject
{
    public GameObject HighlightObject;
    private Renderer objectRenderer;
    private readonly List<Vector3> possibleMoves = new List<Vector3>();
    private bool areMovesHighlighted;

    //states
    protected bool InitialUnitPlacingSet { get { return objectRenderer.enabled; } set { objectRenderer.enabled = value; } }

	// Use this for initialization
	void Start ()
	{
        objectRenderer = GetComponent<Renderer>();
	    InitialUnitPlacingSet = false;
	}
	
	// Update is called once per frame
	void Update () {
        //If it's not the player's turn, exit the function.
        if (!GameState.Instance.IsPlayersTurn) return;

        if (!areMovesHighlighted)
        {
            ComputePossibleMoves();
            HighlightPossibleMoves();
            areMovesHighlighted = true;
        }	    

        
	}

    private void HighlightPossibleMoves()
    {
        foreach (var possibleMove in possibleMoves)
        {
//            Debug.Log(possibleMove.x+","+possibleMove.y);
            var highlight = Instantiate(HighlightObject);
            highlight.transform.position = possibleMove;
        }
    }
    
    private void ComputePossibleMoves()
    {
        possibleMoves.Clear();
        int startRow, endRow, column;
        if (!InitialUnitPlacingSet)
        {
            startRow = 1;
            endRow = GameState.Instance.Rows - 2;
            column = GameState.Instance.Columns - 1;
        }
        else
        {
            startRow = (int)transform.position.y == 1 ? 1 : ((int)transform.position.y) - 1;
            endRow = (int)transform.position.y == GameState.Instance.Rows - 2 ? GameState.Instance.Rows - 2 : ((int)transform.position.y) + 1;
            column = ((int)transform.position.x) - 1;
        }

        for (var i = startRow; i <= endRow; i++)
        {
            possibleMoves.Add(new Vector3(column, i, 0));
        }

    }
}
