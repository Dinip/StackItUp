using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gm;

    [SerializeField]
    private GameObject startMenu;

    [SerializeField]
    private GameObject difficultyMenu;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        difficultyMenu.SetActive(true);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            startMenu.SetActive(true);
            difficultyMenu.SetActive(false);
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

