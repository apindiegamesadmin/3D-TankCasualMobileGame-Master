using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int maxInt;
    public GameObject[] zombies;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        foreach(GameObject zombie in zombies)
        {
            zombie.SetActive(false);
        }
        int randIndex = Random.Range(0, zombies.Length);
        zombies[randIndex].SetActive(true);


        int index = Random.Range(0, maxInt);
        animator.SetInteger("Index", index);
    }
}
