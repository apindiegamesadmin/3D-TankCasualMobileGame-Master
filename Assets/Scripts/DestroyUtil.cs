using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUtil : MonoBehaviour
{
    public void DestroyHelper()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Destroy the game object once it has been disabled in the hiearchy
    /// </summary>
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
