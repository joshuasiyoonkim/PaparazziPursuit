using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


    public class GameController : MonoBehaviour {
    public static GameController instance;

    //Outlets
    public Transform[] spawnPoints;
    public GameObject[] obstaclePrefabs;

    public float timeElasped;
    void Awake() {
        instance = this;
    }

    void Update() {
    //Increment passage of time for each frame of the game
    timeElapsed += Time.deltaTime;

        //Computer Asteroid Delay
    //float decreaseDelayOverTime = maxObstacleDelay - ((maxObstacleDelay - minObstacleDelay)/ 30f * timeElapsed);
    //obstacleDelay = Mathf.Clamp(decreaseDelayOverTime, minObstacleDelay, maxObstacleDelay);

}
}
