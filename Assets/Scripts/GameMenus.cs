using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject winLoseMenu;

    private void OnEnable()
    {
        gameManager.gameOverEvent.AddListener(ShowGameOver);
    }

    private void OnDisable()
    {
        gameManager.gameOverEvent.RemoveListener(ShowGameOver);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameManager.SetPause(false);
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameManager.SetPause(true);
    }

    public void LoadMainMenu()
    {
        gameManager.SetPause(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        gameManager.ResetGame();
        gameManager.SetPause(false);
        winLoseMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowGameOver(bool win)
    {
        winLoseMenu.SetActive(true);

        Time.timeScale = 0f;
        gameManager.SetPause(true);

        TextMeshProUGUI winTextUI = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI healthTextUI = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI piecesTextUI = GameObject.Find("PiecesText").GetComponent<TextMeshProUGUI>();

        winTextUI.SetText(win ? "You Win! :)" : "You Lost! :(");
        winTextUI.color = win ? Color.green : Color.red;

        healthTextUI.SetText($"{gameManager.health} hearts left");
        piecesTextUI.SetText($"{gameManager.pieces} pieces stacked");
    }
}
