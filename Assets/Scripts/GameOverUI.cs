using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI textMesh;


    void Awake()
    {
        panel.SetActive(false);
        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    public void ShowGameOver(string reason)
    {
        panel.SetActive(true);
        textMesh.text = reason;
    }
}
