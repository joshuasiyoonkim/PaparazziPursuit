using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;

    // Scaling variables
    public float initialScale = 0.5f; // Initial scale when the obstacle spawns
    public float minScale = 0.5f; // Minimum scale at the top of the screen
    public float maxScale = 1.5f; // Maximum scale at the bottom of the screen
    public float topScreenY = 10f; // Y position representing the top of the screen
    public float bottomScreenY = -10f; // Y position representing the bottom of the screen

    // Lane switching variables
    public float switchInterval = 1f; // Time interval between lane switches
    public float laneSwitchSpeed = 10f; // Speed of switching lanes
    private float switchTimer;
    private float[] lanePositions = { -3f, -1f, 1f, 3f }; // X positions of lanes
    private int currentLane;

    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentLane = Random.Range(0, lanePositions.Length); // Start in a random lane
        switchTimer = switchInterval;
        
        // Set the initial scale
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
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

        // Update the switch timer
        switchTimer -= Time.deltaTime;
        if (switchTimer <= 0)
        {
            // Switch to a new lane
            currentLane = Random.Range(0, lanePositions.Length);
            switchTimer = switchInterval;
        }

        // Smoothly move towards the current lane position
        Vector2 targetPosition = new Vector2(lanePositions[currentLane], transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
