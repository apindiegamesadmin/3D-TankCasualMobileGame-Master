using System;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    public abstract void Attack(ZombieController tank, AIDetector detector);
    public abstract void Run(ZombieController tank, AIDetector detector);
}