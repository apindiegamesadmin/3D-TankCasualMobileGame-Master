using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectionHandler : MonoBehaviour
{
    public int carindex;
    public static CarSelectionHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
