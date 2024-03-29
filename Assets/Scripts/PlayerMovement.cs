using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeedLeft = 40f;
    public float moveSpeedRight = 40f;
    public float accelerationRate = 70f;
    public float maxSpeed = 150f;
    public float defaultSpeed = 40f;

    private bool isFacingLeft;

    // Start is called before the first frame update
    void Start()
    {
        isFacingLeft = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move left
        if(Input.GetKey(KeyCode.A))
        {
            moveSpeedLeft = Mathf.Min(moveSpeedLeft + accelerationRate * Time.deltaTime, maxSpeed);
            rb.AddForce(Vector2.left * moveSpeedLeft * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            moveSpeedLeft = defaultSpeed;
        }

        //move right
        if (Input.GetKey(KeyCode.D))
        {
            moveSpeedRight = Mathf.Min(moveSpeedRight + accelerationRate * Time.deltaTime, maxSpeed);
            rb.AddForce(Vector2.right * moveSpeedRight * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            moveSpeedRight = defaultSpeed;
        }


        //do the face switching
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            isFacingLeft = !isFacingLeft;
            //do animations
        }

    }

    public bool IsFacingLeft()
    {
        return isFacingLeft;
    }
}
