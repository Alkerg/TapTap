using UnityEngine;
using TMPro;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;

public class CounterManager : MonoBehaviour
{
    public static CounterManager Instance { get; private set; }
    public static event Action<string> Achievement_taps;
    [SerializeField]
    private LeaderboardManager leaderboardManager;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private long playerScore = 0;
    private Coroutine debounceCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        LoginManager.AuthenticationSucceeded += GetUserScoreFromLeaderboard;

        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            GetUserScoreFromLeaderboard();
        }
    }

    void OnDisable()
    {
        LoginManager.AuthenticationSucceeded -= GetUserScoreFromLeaderboard;
    }

    void GetUserScoreFromLeaderboard()
    {
        if(!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //messageText.text = "User is not authenticated. Cannot retrieve score.";
            Debug.Log("User is not authenticated. Cannot retrieve score.");
            return;
        }

        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_tapsleaderboard,
            LeaderboardStart.PlayerCentered,
            1,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            OnLoadedScoreCallback
        );
    }

    void OnLoadedScoreCallback(LeaderboardScoreData data)
    {
        if(data != null && data.Valid)
        {
            long score = data.PlayerScore.value;
            playerScore = score;
            scoreText.text = playerScore.ToString();
        }
        else
        {
            //messageText.text = "Failed to load score from leaderboard.";
            Debug.Log("Failed to load score from leaderboard.");
        }
    }
    
    public void TriggerScoreUpdate()
    {
        long previousScore = playerScore;
        playerScore++;

        if(previousScore < 50 && playerScore >= 50)
        {
            Debug.Log("Achievement unlocked: " + GPGSIds.achievement_get_50_taps);
            Achievement_taps?.Invoke(GPGSIds.achievement_get_50_taps);
        }

        if(previousScore < 100 && playerScore >= 100)
        {
            Debug.Log("Achievement unlocked: " + GPGSIds.achievement_get_100_taps);
            Achievement_taps?.Invoke(GPGSIds.achievement_get_100_taps);
        }

        if(previousScore < 150 && playerScore >= 150)
        {
            Debug.Log("Achievement unlocked: " + GPGSIds.achievement_get_150_taps);
            Achievement_taps?.Invoke(GPGSIds.achievement_get_150_taps);
        }

        if(previousScore < 200 && playerScore >= 200)
        {
            Debug.Log("Achievement unlocked: " + GPGSIds.achievement_get_200_taps);
            Achievement_taps?.Invoke(GPGSIds.achievement_get_200_taps);
        }

        if(previousScore < 250 && playerScore >= 250)
        {
            Debug.Log("Achievement unlocked: " + GPGSIds.achievement_get_250_taps);
            Achievement_taps?.Invoke(GPGSIds.achievement_get_250_taps);
        }

        scoreText.text = playerScore.ToString();
        
        if(debounceCoroutine != null)
        {
            StopCoroutine(debounceCoroutine);
        }

        debounceCoroutine = StartCoroutine(DebounceCoroutine());
    }

    public IEnumerator DebounceCoroutine()
    {
        yield return new WaitForSeconds(1f);
        LeaderboardManager.Instance.PostScoreToLeaderboard(playerScore);
    }
    
}
