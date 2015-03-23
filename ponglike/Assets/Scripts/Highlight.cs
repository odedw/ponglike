using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour
{
    public delegate void HighlighterClicked(GameObject highlighter);
    public event HighlighterClicked OnHighlighterClicked;

    private float initialOpacity;
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>();
	    initialOpacity = spriteRenderer.color.a;
	}

    void OnMouseEnter()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b,1);
    }
    void OnMouseExit()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, initialOpacity);
    }

    void OnMouseDown()
    {
        if (OnHighlighterClicked != null) OnHighlighterClicked(gameObject);
    }
}
