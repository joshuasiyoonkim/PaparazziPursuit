using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVisibilityController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Start with the sprite hidden
    }

    public void ShowSprite()
    {
        spriteRenderer.enabled = true; // Show the sprite when called by the animation event
    }

    public void HideSprite()
    {
        spriteRenderer.enabled = false; // Hide the sprite when called by the animation event
    }
}
