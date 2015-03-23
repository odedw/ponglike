﻿using System;
using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

    public float MoveTime = 0.1f;			//Time it will take object to move, in seconds.
    public LayerMask BlockingLayer;			//Layer on which collision will be checked.
    public float ScoredDelay = 0.5f;
   
    private BoxCollider2D boxCollider; 		//The BoxCollider2D component attached to this object.
    private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.
    private float inverseMoveTime;			//Used to make movement more efficient.
    private Renderer objectRenderer;

    //states
    protected bool isMoving;
    protected abstract bool IsUnitsTurn { get; set; }
    protected bool InitialUnitPlacingSet { get { return objectRenderer.enabled; } set { objectRenderer.enabled = value; } }
    protected bool fogOfWarReset { get; set; }

    //unit specific config
    protected abstract int StartColumn { get; }
    protected abstract int UnitAdvanceDirection { get; }
    protected abstract int EndColumn { get; }
    protected abstract bool DesiredFogOfWarState { get; }

    // Use this for initialization
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / MoveTime;
        objectRenderer = GetComponent<Renderer>();
        InitialUnitPlacingSet = false;
	}

    protected void Move(Vector2 targetDestination)
    {
        //        Debug.Log("Moving to " + targetDestination.x + "," + targetDestination.y);
        isMoving = true;
        if (!InitialUnitPlacingSet)
        {
            transform.position = targetDestination;
            isMoving = false;
            InitialUnitPlacingSet = true;
            return;
        }
        //        Vector2 start = transform.position;
        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        //        boxCollider.enabled = false;
        //Cast a line from start point to end point checking collision on blockingLayer.
        //        hit = Physics2D.Linecast(start, end, blockingLayer);
        //Re-enable boxCollider after linecast
        //        boxCollider.enabled = true;
        //Check if anything was hit
        //        if (hit.transform == null)
        //        {
        //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
        StartCoroutine(SmoothMovement(targetDestination));

        //Return true to say that Move was successful
        //            return true;
        //        }

        //If something was hit, return false, Move was unsuccesful.
        //        return false;
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end)
    {
//        Debug.Log(end.x + "," + end.y);

        var sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            var newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPostion);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        
        //reached other side
        if (Math.Abs(end.x - EndColumn) < float.Epsilon)
        {
            Invoke("Scored", ScoredDelay);
            
        }
        else
        {
            isMoving = false;
        }
    }

    void Scored()
    {
        isMoving = false;
        IsUnitsTurn = false;
        InitialUnitPlacingSet = false;
        fogOfWarReset = false;
    }

    protected void ResetFogOfWar()
    {
        GameManager.Instance.BoardManager.SetAllFogOfWar(DesiredFogOfWarState);
        fogOfWarReset = true;
    }
}
