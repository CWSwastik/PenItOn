using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("🎯 Ball reached the goal!");
            gameController.OnGoalReached();
        }
    }

}
