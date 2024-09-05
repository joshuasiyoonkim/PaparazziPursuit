using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraguys : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    Collider2D _collider;

    // Scaling variables
    public float minScale = 0.5f; // Minimum scale at the top of the screen
    public float maxScale = 1.5f; // Maximum scale at the bottom of the screen
    public float topScreenY = 10f; // Y position representing the top of the screen
    public float bottomScreenY = -10f; // Y position representing the bottom of the screen

    // Speed control
    public float speedMultiplier = 0.5f; // Adjust this value to slow down the cameraguys

    // Delay control
    public float startDelay = 3f; // Time in seconds before the cameraguys start moving
    private float timeElapsed = 0f;

    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        // Disable rendering and collider initially
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= startDelay)
        {
            // Enable rendering and collider after the delay
            if (!_renderer.enabled)
            {
                _renderer.enabled = true;
                _collider.enabled = true;
            }

            // Move the camera down the screen at a slower speed based on the speed multiplier
            //_rb.velocity = Vector2.down * Mathf.Abs(ScrollingBackground.currentScrollSpeed) * speedMultiplier;
            _rb.velocity = Vector2.down * Mathf.Abs(TilemapScroller.currentScrollSpeed) * speedMultiplier;

            // Calculate the normalized position of the object between the top and bottom of the screen
            float normalizedPosition = Mathf.InverseLerp(topScreenY, bottomScreenY, transform.position.y);

            // Calculate the scale factor based on the normalized position
            float scaleFactor = Mathf.Lerp(minScale, maxScale, normalizedPosition);

            // Apply the scaling factor to the object's local scale
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
        else
        {
            // Keep the velocity zero until the start delay has passed
            _rb.velocity = Vector2.zero;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
