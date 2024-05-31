using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstalce : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;

    //Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Debug.Log("speed: " + GameController.speed);
        _rb.velocity = Vector2.down * GameController.speed;
    }


    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}

