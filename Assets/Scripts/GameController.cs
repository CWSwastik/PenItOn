using UnityEngine;
using TMPro;  // for TextMeshPro
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI levelText;

    private static int levelNo = 1;

    [SerializeField] private List<GameLevel> gameLevelList;

    private bool gameStarted = false;
    private bool goalReached = false;
    private float gameDuration = 10f;
    private float timeRemaining;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 0f;
        timeRemaining = gameDuration;

        if (timerText != null)
            timerText.text = $"{timeRemaining:F1}";

        Debug.Log("Game paused. Press SPACE to start.");
        LoadCurrentLevel();
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
            // Time.timeScale = 0f;
            if (gameOverUI != null)
                gameOverUI.showLevelComplete();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.ShowGameOver("Game Over! Time ran out.");
            
    }

    private void LoadCurrentLevel()
    {
        levelText.text = $"Level {levelNo}";
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNo)
            {
                Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
            }
        }
    }

    public void GoToNextLevel()
    {
        levelNo++;
        Debug.Log("Go TO NEXT LEVEL PLS");
        SceneManager.LoadScene(0);
    }
    
    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }
}
