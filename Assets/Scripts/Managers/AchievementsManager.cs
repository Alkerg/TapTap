using UnityEngine;
using GooglePlayGames;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance { get; private set; }
    void Awake()
    {
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

    void OnEnable()
    {
        CounterManager.Achievement_taps += UnlockAchievementXTaps;
    }

    void OnDisable()
    {
        CounterManager.Achievement_taps -= UnlockAchievementXTaps;
    }

    void UnlockAchievementXTaps(string achievementId)
    {
        PlayGamesPlatform.Instance.ReportProgress(achievementId, 100.0f, success =>
        {
            if (success)
            {
                Debug.Log("Achievement unlocked: " + achievementId);
            }
            else
            {
                Debug.Log("Failed to unlock achievement: " + achievementId);
            }
        });
    }

    public void ShowAchievementsUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

}
