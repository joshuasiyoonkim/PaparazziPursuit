using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecovery : MonoBehaviour
{
    public float maxCharacterHeight = -2.5f;
    public float recoverySpeed = 3f;
    public float cooldownTime = 5f;

    private bool isRecovering = false;
    private float lastHitTime;

    private Coroutine recoveryCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < maxCharacterHeight && !isRecovering && Time.time - lastHitTime >= cooldownTime)
        {
            recoveryCoroutine = StartCoroutine(RecoverPosition());
        }
    }

    public void OnPlayerHit()
    {
        //if player has been hit, stop coroutine and track 3 seconds before recovering again
        Debug.Log("Player hit, wait 3 seconds before recovering");
        lastHitTime = Time.time;

        if (recoveryCoroutine != null)
        {
            StopCoroutine(recoveryCoroutine);
            recoveryCoroutine = null;
        }
        isRecovering = false;
    }

    IEnumerator RecoverPosition()
    {
        isRecovering = true;

        while (transform.position.y < maxCharacterHeight)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, maxCharacterHeight, Time.deltaTime * recoverySpeed));
            yield return null;
        }

        transform.position = new Vector2(transform.position.x, maxCharacterHeight);

        isRecovering = false;
    }
}
