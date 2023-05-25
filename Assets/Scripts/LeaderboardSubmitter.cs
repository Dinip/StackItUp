using LootLocker.Requests;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class LeaderboardSubmitter : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    [SerializeField]
    private TMP_InputField playerName;

    [SerializeField]
    private GameObject submitButton;

    void Start()
    {

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Started");
            }
            else
            {
                Debug.Log("Session Failed");
            };
        });
    }

    public void SubmitScore()
    {
        submitButton.SetActive(false);

        //get date in format: 2021-03-01 12:01
        string date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        //normalize player name to lowercase and only allow alphanumeric characters
        playerName.text = Regex.Replace(playerName.text, "[^a-zA-Z0-9]", "").ToLower();

        if (playerName.text.Length == 0)
        {
            playerName.text = "anonymous";
        }

        if (playerName.text.Length > 20)
        {
            playerName.text = playerName.text.Substring(0, 20);
        }

        if (gameManager.pieces <= 0) return;

        LootLockerSDKManager.SubmitScore(playerName.text, gameManager.pieces, Utils.ConvertDifficultyToLeaderboard(gameManager.difficulty), date, (response) =>
        {
            if (!response.success)
            {
                submitButton.SetActive(true);
            }
        });
    }
}