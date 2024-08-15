using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class papscript : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        // No need for instance if each GameObject manages its own papscript
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            Debug.Log("Animator exists on " + gameObject.name);
        }
        else
        {
            Debug.Log("Animator doesn't exist on " + gameObject.name + " womp womp");
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            Debug.Log("trigger set");
            _animator.SetTrigger("spawnPap");
        }
    }

    public IEnumerator SignalAnimation(int laneIndex, string animationName)
    {
        Debug.Log("Starting animation " + animationName + " on " + gameObject.name);
        _animator.SetTrigger(animationName);

        // Wait until the Animator transitions to the desired state
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsTag(animationName) && !_animator.IsInTransition(0))
        {
            yield return null;
        }

        Debug.Log("Animation " + animationName + " has started on " + gameObject.name);

        // Wait until the animation has finished
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f || _animator.IsInTransition(0))
        {
            yield return null;
        }

        Debug.Log("Animation finished in lane " + laneIndex + " on " + gameObject.name);
    }



}
