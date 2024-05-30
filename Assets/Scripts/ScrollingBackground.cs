using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float minScrollSpeed = -2f;
    public float maxScrollSpeed = -15f;
    public float delay = 30f;
    public float scrollSpeed;
    public float offset;
    private Material mat;
    public float timeElapsed;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {

        timeElapsed += Time.deltaTime;

        //calculate speed and increase over time
        float increaseScrollSpeed = minScrollSpeed + ((maxScrollSpeed - minScrollSpeed) / delay * timeElapsed);
        scrollSpeed = Mathf.Clamp(increaseScrollSpeed, maxScrollSpeed, minScrollSpeed);


        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }

}
