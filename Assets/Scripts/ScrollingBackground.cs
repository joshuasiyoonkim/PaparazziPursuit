using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float minScrollSpeed = -10f; // Higher negative value for faster initial speed
    public float maxScrollSpeed = -30f; // Negative for upward scrolling
    public float delay = 10f;
    public float scrollSpeed;
    public float offset;
    private Material mat;
    private float timeElapsed;

    public static float currentScrollSpeed; // Public static variable to expose the scroll speed

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = 0f; // Initialize offset
        scrollSpeed = minScrollSpeed; // Start at the minimum speed
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Calculate speed and increase over time
        float increaseScrollSpeed = minScrollSpeed + ((maxScrollSpeed - minScrollSpeed) / delay * timeElapsed);
        scrollSpeed = Mathf.Clamp(increaseScrollSpeed, minScrollSpeed, maxScrollSpeed); // Adjusted clamp range

        currentScrollSpeed = scrollSpeed; // Update the static variable

        // Adjust offset for upward scrolling
        offset += Time.deltaTime * Mathf.Abs(scrollSpeed) / 6f; // Adjusted to slow down the scrolling speed
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
