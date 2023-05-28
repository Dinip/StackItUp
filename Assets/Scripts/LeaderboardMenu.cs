using LootLocker.Requests;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardMenu : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    [SerializeField]
    private TMP_Text[] entries;

    [SerializeField]
    private GameObject difficultyButton;

    private Difficulty _selectedDifficulty = Difficulty.Easy;

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Started");
                LoadLeaderboard(Difficulty.Easy);
            }
            else
            {
                Debug.Log("Session Failed");
            };
        });
    }

    private void LoadLeaderboard(Difficulty difficulty)
    {
        LootLockerSDKManager.GetScoreList(Utils.ConvertDifficultyToLeaderboard(difficulty), 10, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    entries[i].text = $"{members[i].rank:D2}.{members[i].member_id.Substring(0, members[i].member_id.Length > 8 ? 8 : members[i].member_id.Length)}-{members[i].score}";
                }

                if (members.Length < entries.Length)
                {
                    for (int i = members.Length; i < entries.Length; i++)
                    {
                        entries[i].text = $"{(i + 1):D2}.NONE";
                    }
                }
            }
            else
            {
                Debug.Log("Leaderboard Retrieval Failed");
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i].text = $"{(i + 1):D2}.NONE";
                }
            }
        });
    }

    public void SelectLeaderboardDifficulty()
    {
        _selectedDifficulty = (Difficulty)(((int)_selectedDifficulty + 1) % Enum.GetNames(typeof(Difficulty)).Length);

        LoadLeaderboard(_selectedDifficulty);

        difficultyButton.GetComponentInChildren<TMP_Text>().text = $"{_selectedDifficulty}";
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
