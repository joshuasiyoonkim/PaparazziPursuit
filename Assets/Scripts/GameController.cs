using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public NumberFont numberFont;
    public static bool isCameraShown = false;
    public static float speed;

    // Outlets
    public Transform[] obstacleSpawnPoints;
    public Transform[] cameraSpawnPoints;
    public GameObject[] obstaclePrefabs;
    public GameObject cameraPrefab;

    // For tracking player score
    public TMP_Text textScore;
    public static float score = 0f;

    public float maxCameraDelay = 4f;
    public float minCameraDelay = 0.5f; // Lowered min delay for faster spawns
    public float cameraDelay;

    public float maxObstacleDelay = 3f;
    public float minObstacleDelay = 0.2f; // Lowered min delay for faster spawns
    public float obstacleDelay;

    public float timeElapsed = 0f;

    // This is for obstacles
    public float minSpeed = 5f;
    public float maxSpeed = 15f; // Increased max speed for more challenge

    public float delay = 20f; // Lowered delay for quicker difficulty increase
    public float speedDelay = 30f; // Lowered speed delay for quicker speed increase

    private float highScore = 0f;

    public GameObject papAnim1;
    public GameObject papAnim2;
    private papscript papScript1;
    private papscript papScript2;

    void Awake() {
        instance = this;
        papScript1 = papAnim1.GetComponent<papscript>();
        papScript2 = papAnim2.GetComponent<papscript>();
    }

    private void Start()
    {
        StartCoroutine("CameraSpawnTimer");
        StartCoroutine("ObstacleSpawnTimer");
        isCameraShown = false;
        score = 0f;
        UpdateDisplay();
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    void Update() {
        // Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        // Compute Camera Delay
        float decreaseDelayOverTime = maxCameraDelay - ((maxCameraDelay - minCameraDelay) / delay * timeElapsed);
        cameraDelay = Mathf.Clamp(decreaseDelayOverTime, minCameraDelay, maxCameraDelay);

        // Obstacle Delay
        float decreaseDelayOverTimeObstacle = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay) / delay * timeElapsed);
        obstacleDelay = Mathf.Clamp(decreaseDelayOverTimeObstacle, minObstacleDelay, maxObstacleDelay);

        // Calculate obstacle speed and increase over time
        // This gets used in obstacle.cs
        float increaseTime = minSpeed + ((maxSpeed - minSpeed) / speedDelay * timeElapsed);
        speed = Mathf.Clamp(increaseTime, minSpeed, maxSpeed);

        if (Time.timeScale > 0)
        {
            score += (Time.deltaTime * 100);
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        if (numberFont != null)
        {
            numberFont.SetNumber(Mathf.FloorToInt(score));
        }
        else
        {
            Debug.LogError("NumberFont reference is not set in the GameManager script.");
        }
        //textScore.text = Mathf.FloorToInt(score).ToString();
    }

    void SpawnObstacle() {
        int randomObstacleSpawnIndex = Random.Range(0, obstacleSpawnPoints.Length);
        Transform randomObstacleSpawnPoint = obstacleSpawnPoints[randomObstacleSpawnIndex];
        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject randomObstaclePrefab = obstaclePrefabs[randomObstacleIndex];

        Instantiate(randomObstaclePrefab, randomObstacleSpawnPoint.position, Quaternion.identity);
    }


    public void SpawnObstacleInLane(int laneIndex)
    {
        Debug.Log("lane index: " + laneIndex);
        Transform randomSpawnPoint = cameraSpawnPoints[laneIndex];
        GameObject spawnedCamera = Instantiate(cameraPrefab, randomSpawnPoint.position, Quaternion.identity);

        // Rotate the camera based on the lane
        if (laneIndex == 1) // Right side
        {
            spawnedCamera.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else // Left side
        {
            spawnedCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        isCameraShown = false;
    }


    private IEnumerator StartAnimationAndSpawnObstacle(int laneIndex)
    {
        Debug.Log("start aniimation coroutine started: " + laneIndex);
        if (laneIndex == 0)
        {
            // Start the animation and wait for it to complete
            yield return StartCoroutine(papScript1.SignalAnimation(laneIndex, "spawnPap"));
        }
        else
        {
            // Start the animation and wait for it to complete
            yield return StartCoroutine(papScript2.SignalAnimation(laneIndex, "spawnPap"));
        }

        // After the animation is complete, spawn the obstacle
        SpawnObstacleInLane(laneIndex);
    }



    IEnumerator CameraSpawnTimer()
    {
        while(true)
        {
            if (!isCameraShown)
            {
                isCameraShown = true;
                yield return new WaitForSeconds(cameraDelay);
                Debug.Log("spawning camera");
                yield return StartCoroutine(SpawnCamera());
            }
            else
            {
                Debug.Log("camera is already being shown");
                yield return null;
            }
            // After spawning, wait for a progressively shorter delay before next spawn
            yield return new WaitForSeconds(Random.Range(minCameraDelay, cameraDelay));
        }
    }

    IEnumerator SpawnCamera()
    {
        Debug.Log("SpawnCamera coroutine started");
        int randomSpawnIndex = Random.Range(0, cameraSpawnPoints.Length);

        // Start the animation coroutine and wait for it to finish
        yield return StartCoroutine(StartAnimationAndSpawnObstacle(randomSpawnIndex));
    }

    IEnumerator ObstacleSpawnTimer() {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minObstacleDelay, obstacleDelay));
            SpawnObstacle();
        }
    }

    public void EarnPoints(int pointAmount)
    {
        score += pointAmount;
        UpdateDisplay();
    }

    public void checkHighScore()
    {
        UIController.finalScore = score;
        if(score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();
            //UIController.highScoreText.text = Mathf.FloorToInt(score).ToString();
        }
    }
}
