using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public void CheckCarCount(int count)
    {
        if (count >= 5)
        {
            Debug.Log("You have achievement.");
        }
    }
}
