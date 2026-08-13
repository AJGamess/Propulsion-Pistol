using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text timerText;

    [Header("Timer Elements")]
    private bool isTimerRunning = true;
    private float timer = 0f;

    [Header("Level Complete References")]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject victoryCamera;

    [HideInInspector] public GameState currentGameState = GameState.Playing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check current game state
        switch (currentGameState)
        {
            case GameState.MainMenu:
                // Handle main menu logic
                break;
            case GameState.Playing:
                // Handle gameplay logic
                Timer();
                // When Player presses the reset button, reset the level
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ResetLevel();
                }
                //When Player presses the pause button, pause the game
                if (Input.GetKeyDown(KeyCode.P))
                {
                    currentGameState = GameState.Paused;
                    Time.timeScale = 0f; // Pause the game
                }
                break;
            case GameState.Paused:
                // Handle pause menu logic
                // When Player presses the pause button or clicks resume button, resume the game
                if (Input.GetKeyDown(KeyCode.P)) //TODO: Add a resume button to the pause menu and call this function when the button is clicked
                {
                    currentGameState = GameState.Playing;
                    Time.timeScale = 1f; // Resume the game
                }
                break;
            case GameState.LevelComplete:
                // Handle game over logic
                LevelComplete();
                break;
        }

    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        LevelComplete
    }

    public void ResetLevel()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
    void Timer()
    {
        //Create a timer that counts up in seconds and miliseconds and displays it on the screen with a UI Text element
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }

    public void LevelComplete()
    {
        isTimerRunning = false;
        Debug.Log("Level Complete in: " + timerText.text);

        // Disable the player camera and enable the victory camera
        if (playerCamera != null && victoryCamera != null)
        {
            playerCamera.SetActive(false);
            victoryCamera.SetActive(true);
        }
        else
        {
            Debug.LogError("Player Camera or Victory Camera is not assigned in the GameManager.");
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
        }
        else
        {
            Debug.LogError("Level Complete UI is not assigned in the GameManager.");
        }
    }
}
