using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    [SerializeField]
    private GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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

    public void LoadMenu()
    {
        gameManager.SetPause(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        //gm.ResetGame();
        Time.timeScale = 1f;
        //endMenuUI.SetActive(false);
        //gameUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
