using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float minScrollSpeed = -2f; // Negative for upward scrolling
    public float maxScrollSpeed = -10f; // Negative for upward scrolling
    public float delay = 30f;
    public float scrollSpeed;
    public float offset;
    private Material mat;
    public float timeElapsed;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = 0f; // Initialize offset
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Calculate speed and increase over time
        float increaseScrollSpeed = minScrollSpeed + ((maxScrollSpeed - minScrollSpeed) / delay * timeElapsed);
        scrollSpeed = Mathf.Clamp(increaseScrollSpeed, maxScrollSpeed, minScrollSpeed); // Adjusted clamp range

        // Adjust offset for upward scrolling
        offset -= Time.deltaTime * scrollSpeed / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
