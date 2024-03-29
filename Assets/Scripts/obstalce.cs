using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstalce : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;

    //State Tracking
    float randomSpeed;

    //Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(0.5f, 3f);
    }

    void Update()
    {
        _rb.velocity = Vector2.down * randomSpeed;
    }


    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}

