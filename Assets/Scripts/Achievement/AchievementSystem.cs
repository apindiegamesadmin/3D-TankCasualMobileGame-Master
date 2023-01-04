using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    public GameObject achievementPanel;

    private void Awake()
    {
        achievementPanel.SetActive(false);
    }

    public void CheckCarCount(int count)
    {
        if (count >= 5)
        {
            Debug.Log("You have unlocked first achievement.");
        }
    }

    public void AchievementOnAndOff()
    {
        achievementPanel.SetActive(true);
    }
}
