using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDriveGame;

public class ZombieController : MonoBehaviour
{
    [SerializeField] ZombieAIBehaviour zombieAIBehaviour;

    [Tooltip("The function that runs when this object is touched by the target")]
    public string attackFunction = "ChangeScore";

    [Tooltip("The parameter that will be passed with the function")]
    public float attackPoint = 100;

    [Tooltip("The target object that the function will play from")]
    public string functionTarget = "GameController";

    [Tooltip("The effect that is created at the location of this object when it is touched")]
    public Transform touchEffect;

    public int maxInt;
    public GameObject[] zombies;
    [Tooltip("The function that runs when this object is touched by the target")]
    public string touchFunction = "ChangeScore";

    [Tooltip("The parameter that will be passed with the function")]
    public float functionParameter = 100;

    Transform target;

    AIDetector aiDetector;
    Animator animator;
    bool isDead;

    private void Awake()
    {
        aiDetector = GetComponentInChildren<AIDetector>();
        animator = GetComponent<Animator>();
        isDead = false;
    }
    void Start()
    {
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(false);
        }
        int randIndex = Random.Range(0, zombies.Length);
        zombies[randIndex].SetActive(true);

        float index = Random.Range(0.0f, 1.0f);
        animator.SetFloat("AttackIndex", index);
        index = Random.Range(0.0f, 1.0f);
        animator.SetFloat("RunIndex", index);
    }

    private void FixedUpdate()
    {
        if (aiDetector.TargetVisible && aiDetector.Target != null && target == null)
        {
            target = aiDetector.Target;
            float distance = Vector3.Distance(transform.position, target.position);
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            zombieAIBehaviour.Run(this, aiDetector);
            Debug.Log("Running! 1");
        }
        else if (target != null)
        {
            target = aiDetector.Target;
            float distance = Vector3.Distance(transform.position, target.position);
            if (Mathf.Abs(distance) <= 10f)
            {
                //Attack();
                animator.SetBool("Attack", false);
                animator.SetBool("Run", true);
                Debug.Log("Running! 2");
                zombieAIBehaviour.Attack(this, aiDetector);
            }
            else if (Mathf.Abs(distance) <= 4f)
            {
                //Attack();
                animator.SetBool("Attack", true);
                animator.SetBool("Run", false);
                Debug.Log("Running! 2");
                zombieAIBehaviour.Attack(this, aiDetector);
            }
            else
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Run", true);
                Debug.Log("Running! 3");
                zombieAIBehaviour.Run(this, aiDetector);
            }
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            zombieAIBehaviour.Run(this, aiDetector);
        }
        // else if (isDead)
        // {
        //     animator.SetBool("Attack", false);
        //     animator.SetBool("Run", false);
        //     target = null;
        // }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
        }
    }

    void Attack()
    {
        // Check that we have a target tag and function name before running
        if (attackFunction != string.Empty)
        {
            // Run the function
            GameObject.FindGameObjectWithTag("GameController").SendMessage(attackFunction, attackPoint);
            if (touchEffect) Instantiate(touchEffect, transform.position, transform.rotation);
        }
    }

    public void Die()
    {
        isDead = true;
        zombieAIBehaviour.moveSpeed = 0f;
        animator.SetTrigger("Die");
        Debug.Log("Die!!!!!");
        float index = Random.Range(0.0f, 1.0f);
        animator.SetFloat("DeadIndex", index);

        // Check that we have a target tag and function name before running
        if (touchFunction != string.Empty)
        {
            // Run the function
            GameObject.FindGameObjectWithTag("GameController").SendMessage(touchFunction, functionParameter);
        }

        StartCoroutine(delayDie());
    }

    IEnumerator delayDie()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
