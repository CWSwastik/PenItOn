using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNo;

    public int GetLevelNumber()
    {
        return levelNo;
    }
}
