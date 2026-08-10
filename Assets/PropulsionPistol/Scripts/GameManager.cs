using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text timerText;

    GameState currentGameState = GameState.Playing;
    float timer = 0f;
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
            case GameState.GameOver:
                // Handle game over logic
                break;
        }
        
    }

    enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    void ResetLevel()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
    void Timer()
    {
        //Create a timer that counts up in seconds and miliseconds and displays it on the screen with a UI Text element
        timer += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

    }
}
