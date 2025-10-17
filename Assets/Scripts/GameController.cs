using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool gameStarted = false;

    void Start()
    {
        // Start paused
        Time.timeScale = 0f;
        Debug.Log("Game paused. Press SPACE to start.");
    }

    void Update()
    {
        // Start game when Space is pressed
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        Debug.Log("Game started!");
    }

}
