using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //true = started, false = ended
    public static string gameStatus;

    public GameObject startScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    void Start()
    {
        if (gameStatus == "Restart")
        {
            startScreen.SetActive(false);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            gameStatus = "Playing";
        }
        else
        {
            startScreen.SetActive(true);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            Time.timeScale = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameStatus != "Pause")
            {
                PauseGame();
            }
        }
    }


    public void StartGame()
    {
        Time.timeScale = 1;
        startScreen.SetActive(false);
        gameStatus = "Playing";
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        gameStatus = "Pause";
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameStatus = "Playing";
    }

    public void RestartGame()
    {
        gameStatus = "Restart";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        gameStatus = "Quit";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

}