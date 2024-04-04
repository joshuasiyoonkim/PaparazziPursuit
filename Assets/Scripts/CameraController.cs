using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float playerYPos = -1f;
    public LayerMask cameraLayer;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= playerYPos)
        {
            Vector2 rayDirection = PlayerMovement.instance.isFacingLeft ? Vector2.left : Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(player.position, rayDirection, Mathf.Infinity, cameraLayer);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Player loses!");
            }
        }
    }
}
