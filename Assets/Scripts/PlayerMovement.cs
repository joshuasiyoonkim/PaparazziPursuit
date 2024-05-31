using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    Rigidbody2D rb;
    Animator _animator;

    public float moveSpeedLeft = 40f;
    public float moveSpeedRight = 40f;
    public float accelerationRate = 70f;
    public float maxSpeed = 150f;
    public float defaultSpeed = 40f;

    public bool isFacingLeft;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFacingLeft = true;
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("isFacingLeft", isFacingLeft);
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
            _animator.SetBool("isFacingLeft", isFacingLeft);
            Debug.Log("currently facing left: " + isFacingLeft);
        }

    }

    private void OnBecameInvisible()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
