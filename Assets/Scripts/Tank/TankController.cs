using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public AimTurret aimTurret;
    public Turret turret;
    public bool canShoot = true;
    AIDetector aiDetector;


    private void Awake()
    {
        aimTurret = GetComponent<AimTurret>();
        turret = GetComponentInChildren<Turret>();
        aiDetector = GetComponent<AIDetector>();
    }

    private void Update()
    {
        if (aiDetector.TargetVisible && aiDetector.Target != null)
        {
            HandleTurretMovement(aiDetector.Target.position);
            HandleShoot();
        }
    }

    public void HandleShoot()
    {
        if (canShoot)
        {
            turret.Shoot();
        }

    }

    public void HandleTurretMovement(Vector3 pointerPosition)
    {
        aimTurret.Aim(pointerPosition);
    }
}
