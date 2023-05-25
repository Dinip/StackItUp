using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] entries;

    [SerializeField]
    private GameObject[] difficultyButtons;

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Started");
                SelectLeaderboardDifficulty(0);
            }
            else
            {
                Debug.Log("Session Failed");
            };
        });
    }

    private void LoadLeaderboard(Difficulty difficulty)
    {
        LootLockerSDKManager.GetScoreList(Utils.ConvertDifficultyToLeaderboard(difficulty), 7, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    entries[i].text = $"{members[i].rank}.{members[i].member_id.Substring(0, members[i].member_id.Length > 9 ? 9 : members[i].member_id.Length)}-{members[i].score}";
                }

                if (members.Length < entries.Length)
                {
                    for (int i = members.Length; i < entries.Length; i++)
                    {
                        entries[i].text = $"{i + 1}.NONE";
                    }
                }
            }
            else
            {
                Debug.Log("Leaderboard Retrieval Failed");
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i].text = $"{i + 1}.NONE";
                }
            }
        });
    }

    public void SelectLeaderboardDifficulty(int difficulty)
    {
        LoadLeaderboard((Difficulty)difficulty);
        
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            difficultyButtons[i].GetComponent<Animator>().SetBool("Selected", i == difficulty);
        }
    }
}
