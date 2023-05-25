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
    private GameObject winMenu;

    [SerializeField]
    private GameObject loseMenu;

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
        Cursor.visible = false;
        gameManager.SetPause(false);
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
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
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowGameOver(bool win)
    {
        winMenu.SetActive(win);
        loseMenu.SetActive(!win);

        Time.timeScale = 0f;
        Cursor.visible = true;
        gameManager.SetPause(true);

        TextMeshProUGUI healthTextUI = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI piecesTextUI = GameObject.Find("PiecesText").GetComponent<TextMeshProUGUI>();

        healthTextUI.SetText($"{gameManager.health} hearts left");
        piecesTextUI.SetText($"{gameManager.pieces} pieces stacked");
    }
}
