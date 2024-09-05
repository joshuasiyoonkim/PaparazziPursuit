using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public AudioController audioControllerScript;

    public TMP_Text highScoreText;

    // This is for the game over screen
    public TMP_Text scoreText;

    // This is for the game score
    public TMP_Text gameScore;

    // True = started, false = ended
    public static string gameStatus;
    public static float finalScore;

    public GameObject startScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    public GameObject newspaperImage;
    public GameObject uiElements;

    void Start()
    {
        if (gameStatus == "Restart")
        {
            audioControllerScript.PlayAudio();
            startScreen.SetActive(false);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            uiElements.SetActive(false);
            Time.timeScale = 1;
            gameStatus = "Playing";
        }
        else
        {
            gameStatus = "StartScreen";
            startScreen.SetActive(true);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            Time.timeScale = 0;
        }
        Debug.Log("Current high score: " + PlayerPrefs.GetFloat("HighScore", 0f).ToString());
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

        //if (gameStatus == "Playing")
        //{
        //    audioControllerScript.PlayAudio();
        //}
        //else
        //{
        //    audioControllerScript.StopAudio();
        //}

        gameScore.text = Mathf.FloorToInt(GameController.score).ToString();
    }

    public void StartGame()
    {
        audioControllerScript.PlayAudio();
        Time.timeScale = 1;
        startScreen.SetActive(false);
        gameStatus = "Playing";
    }

    public void PauseGame()
    {
        audioControllerScript.StopAudio();
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        gameStatus = "Pause";
    }

    public void ResumeGame()
    {
        audioControllerScript.PlayAudio();
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
        audioControllerScript.StopAudio();
        // Hide the game score UI and show the game over screen, but don't display the score yet
        gameScore.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);

        Animator animator = newspaperImage.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("ShowNewspaper");
            StartCoroutine(WaitForAnimation(animator, "NewspaperAnimator"));
        }
    }

    private IEnumerator WaitForAnimation(Animator animator, string animationName)
    {
        // Wait for the animation to start
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        // Wait for the animation to complete
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Show the score and high score after the animation completes
        scoreText.text = Mathf.FloorToInt(finalScore).ToString();
        highScoreText.text = Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore", 0f)).ToString();
        
        // Display UI elements
        uiElements.SetActive(true);
        Time.timeScale = 0;
    }
}
