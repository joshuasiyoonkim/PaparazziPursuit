using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraguys : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;

    // Scaling variables
    public float minScale = 0.5f; // Minimum scale at the top of the screen
    public float maxScale = 1.5f; // Maximum scale at the bottom of the screen
    public float topScreenY = 10f; // Y position representing the top of the screen
    public float bottomScreenY = -10f; // Y position representing the bottom of the screen

    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the obstacle down the screen
        _rb.velocity = Vector2.down * GameController.speed;

        // Calculate the normalized position of the object between the top and bottom of the screen
        float normalizedPosition = Mathf.InverseLerp(topScreenY, bottomScreenY, transform.position.y);

        // Calculate the scale factor based on the normalized position
        float scaleFactor = Mathf.Lerp(minScale, maxScale, normalizedPosition);

        // Apply the scaling factor to the object's local scale
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
