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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowInitialMenu();
        }
    }
}

[Serializable]
public enum Difficulty : int
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

[Serializable]
public enum MouseMode : int
{
    Normal = 0,
    Toggle = 1,
    Hold = 2,
    Hold_Inv = 3
}
