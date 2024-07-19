using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public UIController uiController;
    public GameController gameController;

    public Transform player;
    public float playerYPos = -1f;
    public LayerMask cameraLayer;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        uiController = FindObjectOfType<UIController>();
        gameController = FindObjectOfType<GameController>();
    }
    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= playerYPos)
        {
            Vector2 rayDirection;
            if(PlayerMovement.instance.isFacingLeft)
            {
                rayDirection = Vector2.left;
                RayCastCheck(rayDirection);

            } else if(PlayerMovement.instance.isFacingRight)
            {
                rayDirection = Vector2.right;
                RayCastCheck(rayDirection);
            } else
            {
                RayCastCheck(Vector2.left);
                RayCastCheck(Vector2.right);
            }
        }
    }

    private void RayCastCheck(Vector2 rayDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, rayDirection, Mathf.Infinity, cameraLayer);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            //player loses, show game over screen
            gameController.checkHighScore();
            uiController.GameOver();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
