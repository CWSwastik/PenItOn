using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;

    [SerializeField] private TextMeshProUGUI textMesh;


    public Action nextButtonClickAction;
    void Awake()
    {
        panel.SetActive(false);
        nextButton.onClick.AddListener(() =>
        {
            nextButtonClickAction();
            //SceneManager.LoadScene(0);
        });
    }

    public void ShowGameOver(string reason)
    {
        panel.SetActive(true);
        textMesh.text = reason;
        nextButtonClickAction = GameController.Instance.RetryLevel;
        nextButtonTextMesh.text = "RESTART";
    }

    public void showLevelComplete()
    {
        panel.SetActive(true);
        textMesh.text = "Level Successfully completed!";
        nextButtonClickAction = GameController.Instance.GoToNextLevel;
        nextButtonTextMesh.text = "CONTINUE";
    }


}
