using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRotator : MonoBehaviour
{
    public float speed = 100f;
    public float angle;
    void Update()
    {
        angle += speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
