using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteVisibilityController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    public string animationName;
    private int loopCount = 0;
    public int maxLoops = 3; // Set to 2 for looping twice

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Start with the sprite hidden
    }

    public void ShowSprite()
    {
        spriteRenderer.enabled = true; // Show the sprite at the start of the animation
    }

    public void CheckAndLoopOrHide()
    {
        Debug.Log("check loop function called");
        loopCount++;
        if (loopCount < maxLoops)
        {
            // Restart the animation to loop again
            animator.Play(animationName, 0, 0);
        }
        else
        {
            // After the final loop, hide the sprite
            HideSprite();
            animator.SetTrigger("stopAnim");
            loopCount = 0;
        }
    }

    public void HideSprite()
    {
        spriteRenderer.enabled = false; // Hide the sprite after the final loop
    }
}
