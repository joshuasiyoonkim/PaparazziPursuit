using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    public GameObject[] animationPoint;
    public string animationName;
    public GameController gameController;

    //private void Start()
    //{
    //    for(int i = 0; i < animationPoint.Length; i++)
    //    {
    //        GameObject point = animationPoint[i];
    //        SpriteRenderer spriteRenderer = point.GetComponent<SpriteRenderer>();
    //        spriteRenderer.enabled = false;
    //    }
    //}


    public IEnumerator PlayAnimation()
    {
        int randomIndex = Random.Range(0, animationPoint.Length);
        Debug.Log("camera index: " + randomIndex);
        GameObject selectedObject = animationPoint[randomIndex];

        Animator animator = selectedObject.GetComponent<Animator>();
        //SpriteRenderer renderer = selectedObject.GetComponent<SpriteRenderer>();

        if (animator != null)
        {
            //renderer.enabled = true;
            // Play the animation at the selected game object's location
            Debug.Log("setting animation trigger: " + animationName);
            animator.SetTrigger(animationName);

            // Wait until the animation is done
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !animator.IsInTransition(0))
            {
                yield return null; // Wait until the end of the frame before checking again
            }
            //renderer.enabled = false;

            gameController.SpawnCamera(randomIndex);
        }
        else
        {
            Debug.LogWarning("No Animator found on the selected object.");
        }
    }
}
