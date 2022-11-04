using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDriveGame;

public class ZombieController : MonoBehaviour
{
    [SerializeField] ZombieAIBehaviour zombieAIBehaviour;

    public int maxInt;
    public GameObject[] zombies;
    [Tooltip("The function that runs when this object is touched by the target")]
    public string touchFunction = "ChangeScore";

    [Tooltip("The parameter that will be passed with the function")]
    public float functionParameter = 100;

    Transform target;

    AIDetector aiDetector;
    Animator animator;
    private void Awake()
    {
        aiDetector = GetComponentInChildren<AIDetector>();
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

        float index = Random.Range(0.0f, 1.0f);
        animator.SetFloat("AttackIndex", index);
        index = Random.Range(0.0f, 1.0f);
        animator.SetFloat("RunIndex", index);
    }

    private void LateUpdate()
    {
        if (aiDetector.TargetVisible && aiDetector.Target != null && target == null)
        {
            target = aiDetector.Target;
            float distance = Vector3.Distance(transform.position, target.position);
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            zombieAIBehaviour.Run(this, aiDetector);
        }
        else if(target != null)
        {
            target = aiDetector.Target;
            float distance = Vector3.Distance(transform.position, target.position);
            if (Mathf.Abs(distance) <= 5f)
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Run", false);
                zombieAIBehaviour.Attack(this, aiDetector);
            }
            else
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Run", true);
                zombieAIBehaviour.Run(this, aiDetector);
            }
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            zombieAIBehaviour.Run(this, aiDetector);
        }
        else 
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
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
