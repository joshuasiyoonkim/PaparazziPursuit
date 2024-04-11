using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController instance;

    //Outlets
    public Transform[] obstacleSpawnPoints;
    public Transform[] cameraSpawnPoints;
    public GameObject[] obstaclePrefabs;
    public GameObject cameraPrefab;

    public float maxCameraDelay = 10f;
    public float minCameraDelay = 3f;
    public float cameraDelay;

    public float maxObstacleDelay = 8f;
    public float minObstacleDelay = 1f;

    public float obstacleDelay;

    public float timeElapsed;
    void Awake() {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine("CameraSpawnTimer");
        StartCoroutine("ObstacleSpawnTimer");
    }

    void Update() {
        //Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        //Computer Asteroid Delay
        float decreaseDelayOverTime = maxCameraDelay - ((maxCameraDelay - minCameraDelay) / 30f * timeElapsed);
        cameraDelay = Mathf.Clamp(decreaseDelayOverTime, minCameraDelay, maxCameraDelay);

        //Obstacle Delay
        float decreaseDelayOverTimeObstacle = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay) / 30f * timeElapsed);
        obstacleDelay = Mathf.Clamp(decreaseDelayOverTimeObstacle, minObstacleDelay, maxObstacleDelay);

    }

    void SpawnObstacle() {
        int randomObstacleSpawnIndex = Random.Range(0, obstacleSpawnPoints.Length);
        Transform randomObstacleSpawnPoint = obstacleSpawnPoints[randomObstacleSpawnIndex];
        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject randomObstaclePrefab = obstaclePrefabs[randomObstacleIndex];

        Instantiate(randomObstaclePrefab, randomObstacleSpawnPoint.position, Quaternion.identity);
    }

    void SpawnCamera()
    {
        int randomSpawnIndex = Random.Range(0, cameraSpawnPoints.Length);
        Transform randomSpawnPoint = cameraSpawnPoints[randomSpawnIndex];
        Instantiate(cameraPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator CameraSpawnTimer()
    {
        yield return new WaitForSeconds(cameraDelay);

        SpawnCamera();

        StartCoroutine("CameraSpawnTimer");
    }

    IEnumerator ObstacleSpawnTimer() {
        yield return new WaitForSeconds(obstacleDelay);
        SpawnObstacle();
        StartCoroutine("ObstacleSpawnTimer");
    }
}
