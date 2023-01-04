using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Storesinformation related to a single achievement
/// </summary>
[System.Serializable]
public struct AchievementInfromation
{
    [Tooltip("Name used to unlock/set achievement progress")]
    [SerializeField] public string Key;
    [Tooltip("The display name for an achievement. Shown to the user on the UI.")]
    [SerializeField] public string DisplayName;
    [Tooltip("Description for an achievement. Shown to the user on the UI.")]
    [SerializeField] public string Description;
    [Tooltip("The icon which will be displayed when the achievement is locked")]
    [SerializeField] public Sprite LockedIcon;
    [Tooltip("If true, the lock icon will be overlayed on top of the achieved version.")]
    [SerializeField] public bool LockOverlay;
    [Tooltip("The icon which will be displayed when the achievement is  Achieved")]
    [SerializeField] public Sprite AchievedIcon;
    [Tooltip("Treat the achievement as a spoiler for the game. Hidden from player until unlocked.")]
    [SerializeField] public bool Spoiler;
    [Tooltip("If true, this achievement will count to a certain amount before unlocking. E.g. race a total of 500 km, collect 10 coins or reach a high score of 25.")]
    [SerializeField] public bool Progression;
    [Tooltip("The goal which must be reached for the achievement to unlock.")]
    [SerializeField] public float ProgressGoal;
    [Tooltip("The rate that progress updates will be displayed on the screen e.g. Progress goal = 100 and Notification Frequency = 25. In this example, the progress will be displayed at 25,50,75 and 100.")]
    [SerializeField] public float NotificationFrequency;
    [Tooltip("A string which will be displayed with a progress achievement e.g. $, KM, Miles etc")]
    [SerializeField] public string ProgressSuffix;
}

/// <summary>
/// Stores the current progress and achieved state
/// </summary>
[System.Serializable]
public class AchievementState
{
    public AchievementState(float NewProgress, bool NewAchieved)
    {
        Progress = NewProgress;
        Achieved = NewAchieved;
    }
    public AchievementState() { }

    [SerializeField] public float Progress;             //Progress towards goal
    [SerializeField] public int LastProgressUpdate = 0; //Last achievement notification bracket
    [SerializeField] public bool Achieved = false;      //Is the achievement unlocked
}

/// <summary>
/// Place where an achievement will be displayed on the screen
/// </summary>
public enum AchievementStackLocation
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}