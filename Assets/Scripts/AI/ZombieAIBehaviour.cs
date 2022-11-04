using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZombieAIBehaviour : AIBehaviour
{
    public float fieldOfVisionForShooting = 60;
    public float damping = 10f;
    public float moveSpeed = 100f;
    public float damage = 50f;

    public override void Attack(ZombieController zombie, AIDetector detector)
    {
        //Debug.Log("Attacking");
        //Damage player;
    }
    public override void Run(ZombieController zombie, AIDetector detector)
    {
        //if (TargetInFOV(zombie, detector))
        //{
        //    //Debug.Log("Running");

        //}

        var lookPos = detector.Target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        Vector3 targetPos = new Vector3(detector.Target.transform.position.x, transform.position.y, detector.Target.transform.position.z);
        zombie.transform.position = Vector3.MoveTowards(zombie.transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private bool TargetInFOV(ZombieController zombie, AIDetector detector)
    {
        var direction = detector.Target.position - zombie.transform.position;
        if (Vector2.Angle(zombie.transform.right, direction) < fieldOfVisionForShooting / 2)
        {
            return true;
        }
        return false;
    }
}
