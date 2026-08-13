using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        // Prevent multiple triggers
        if (hasWon) return;

        if(other.CompareTag("Player"))
        {
            hasWon = true;
            Debug.Log("You Win!");

            GameManager gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.currentGameState = GameManager.GameState.LevelComplete;
            }
            else
            {
                Debug.LogError("GameManager not found in the scene.");
            }
        }
    }
}
