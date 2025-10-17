using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("🎯 Ball reached the goal!");
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        Debug.Log("Level Completed!");
        // TODO: Add next-level logic
    }
}
