using UnityEngine;
using System.Collections;

public class OpacityChanger : MonoBehaviour
{
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
}
