using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform camera;

    private void Awake()
    {
        camera = Camera.main.transform;
        if (camera == null)
        {
            Debug.Log("No camera was found!");
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
