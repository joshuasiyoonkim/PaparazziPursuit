using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float playerYPos = -2.5f;
    public LayerMask cameraLayer;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= playerYPos)
        {
            Vector2 rayDirection = playerMovement.IsFacingLeft() ? Vector2.left : Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(player.position, rayDirection, Mathf.Infinity, cameraLayer);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Player loses!");
            }
        }
    }
}
