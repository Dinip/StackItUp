using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gm;

    [SerializeField]
    private GameObject startMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject difficultyMenu;

    [SerializeField]
    private TextMeshProUGUI mouseModeText;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        difficultyMenu.SetActive(true);
    }

    public void ShowSettings()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
        SetMouseModeText();
    }

    public void ChangeMouseMode()
    {
        gm.mouseMode = (MouseMode)(((int)gm.mouseMode + 1) % Enum.GetNames(typeof(MouseMode)).Length);
        SetMouseModeText();
    }

    private void SetMouseModeText()
    {
        mouseModeText.text = $"{gm.mouseMode}";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectDifficulty(int difficulty)
    {
        gm.SetDifficulty((Difficulty)difficulty);
        SceneManager.LoadScene("Game");
    }

    public void ShowInitialMenu()
    {
        startMenu.SetActive(true);
        difficultyMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            ShowInitialMenu();
        }
    }
}