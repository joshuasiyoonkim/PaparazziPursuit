using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScroller : MonoBehaviour
{
    public float minScrollSpeed = 0.5f; // Initial speed of the scrolling background
    public float maxScrollSpeed = 2f; // Maximum speed
    public float delay = 10f; // Time it takes to reach max speed
    public Vector2 tileSize; // Size of your background tile in Unity units

    private Vector3 startPosition;
    private float scrollSpeed;
    private float timeElapsed;

    public static float currentScrollSpeed;

    void Start()
    {
        // Store the initial position of the Tilemap
        startPosition = transform.position;

        // Set initial scroll speed
        scrollSpeed = minScrollSpeed;
    }

    void Update()
    {
        // Update the time elapsed since the start
        timeElapsed += Time.deltaTime;

        // Gradually increase the scroll speed over time
        float increaseScrollSpeed = minScrollSpeed + ((maxScrollSpeed - minScrollSpeed) / delay * timeElapsed);
        scrollSpeed = Mathf.Clamp(increaseScrollSpeed, minScrollSpeed, maxScrollSpeed);

        // Calculate the new Y position for vertical scrolling
        float newPositionY = Mathf.Repeat(Time.time * scrollSpeed, tileSize.y);
        
        // Move the tilemap vertically for scrolling effect
        transform.position = startPosition + Vector3.down * newPositionY;

        currentScrollSpeed = scrollSpeed;
    }

}
