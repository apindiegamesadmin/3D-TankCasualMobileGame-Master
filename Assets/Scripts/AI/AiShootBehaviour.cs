using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShootBehaviour : AIBehaviour
{
    public float fieldOfVisionForShooting = 60;

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (TargetInFOV(tank, detector))
        {
            tank.HandleShoot();
        }
            
        tank.HandleTurretMovement(detector.Target.position);
    }

    public override void StopAction(TankController tank, AIDetector detector)
    {

    }

    private bool TargetInFOV(TankController tank, AIDetector detector)
    {
        foreach(AimTurret turrent in tank.aimTurret)
        {
            var direction = detector.Target.position - turrent.transform.position;
            if (Vector2.Angle(turrent.transform.right, direction) < fieldOfVisionForShooting / 2)
            {
                return true;
            }
        }
        return false;
    }
}
