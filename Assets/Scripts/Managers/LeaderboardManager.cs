using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI messageText;

    void Awake(){
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PostScoreToLeaderboard(long score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, GPGSIds.leaderboard_tapsleaderboard, success =>
        {
            if(success)
            {
                //messageText.text = "Score posted successfully.";
                Debug.Log("Score posted successfully.");
            }
            else
            {
                Debug.Log("Failed to post score.");
            }
        });
    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
}
