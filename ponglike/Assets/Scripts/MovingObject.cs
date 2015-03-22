using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

    public float MoveTime = 0.1f;			//Time it will take object to move, in seconds.
    public LayerMask BlockingLayer;			//Layer on which collision will be checked.
   
    private BoxCollider2D boxCollider; 		//The BoxCollider2D component attached to this object.
    private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.
    private float inverseMoveTime;			//Used to make movement more efficient.

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / MoveTime;
	}

    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
//    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
//    {
//        //Store start position to move from, based on objects current transform position.
//        Vector2 start = transform.position;
//
//        // Calculate end position based on the direction parameters passed in when calling Move.
//        Vector2 end = start + new Vector2(xDir, yDir);
//
//        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
//        boxCollider.enabled = false;
//
//        //Cast a line from start point to end point checking collision on blockingLayer.
//        hit = Physics2D.Linecast(start, end, blockingLayer);
//
//        //Re-enable boxCollider after linecast
//        boxCollider.enabled = true;
//
//        //Check if anything was hit
//        if (hit.transform == null)
//        {
//            //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
//            StartCoroutine(SmoothMovement(end));
//
//            //Return true to say that Move was successful
//            return true;
//        }
//
//        //If something was hit, return false, Move was unsuccesful.
//        return false;
//    }
}
