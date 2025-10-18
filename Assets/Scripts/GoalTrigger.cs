using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindFirstObjectByType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("❌ No GameController found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("🎯 Ball reached the goal!");
            gameController.OnGoalReached();
        }
    }

}
