using UnityEngine;
using TMPro;  // for TextMeshPro

public class GameController : MonoBehaviour
{
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private TextMeshProUGUI timerText; // reference to UI text
    private bool gameStarted = false;
    private bool goalReached = false;
    private float gameDuration = 10f;
    private float timeRemaining;

    void Start()
    {
        Time.timeScale = 0f;
        timeRemaining = gameDuration;

        if (timerText != null)
            timerText.text = $"{timeRemaining:F1}";

        Debug.Log("Game paused. Press SPACE to start.");
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameStarted && !goalReached)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                GameOver();
            }

            if (timerText != null)
                timerText.text = $"{timeRemaining:F1}";
        }
    }

    void StartGame()
    {
        gameStarted = true;
        goalReached = false;
        Time.timeScale = 1f;
        Debug.Log("Game started!");
        timeRemaining = gameDuration;
    }

    public void OnGoalReached()
    {
        if (!goalReached)
        {
            goalReached = true;
            Debug.Log("Goal reached!");
            Time.timeScale = 0f;
            if (gameOverUI != null)
                gameOverUI.ShowGameOver("You won!");
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.ShowGameOver("Game Over! Time ran out.");
    }
}
