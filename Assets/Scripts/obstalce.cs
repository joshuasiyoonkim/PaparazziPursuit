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
    private float[] lanePositions = { -3.8f, -1.3f, 1.2f, 3.8f }; // X positions of lanes
    private int currentLane;

    // Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentLane = System.Array.IndexOf(lanePositions, transform.position.x);
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
            //if on the left most side, change lanes to right
            if(currentLane == 0)
            {
                currentLane = Random.Range(0, 2);
            } else if(currentLane == (lanePositions.Length - 1))
            {
                currentLane = Random.Range(lanePositions.Length - 2, lanePositions.Length);
            } else
            {
                // Switch to a new lane
                int newLane = Random.Range(0, 2) * 2 - 1;
                currentLane = currentLane + newLane;
            }

            //currentLane = Random.Range(0, lanePositions.Length);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the OnPlayerHit method on the PlayerRecovery script
            collision.gameObject.GetComponent<PlayerRecovery>().OnPlayerHit();
        }
    }
}
