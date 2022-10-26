using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public AimTurret[] aimTurret;
    public List<Turret> turrets;
    public bool canShoot = true;
    public bool canMove = true;


    private void Awake()
    {
        if (aimTurret == null || aimTurret.Length == 0)
        {
            aimTurret = GetComponentsInChildren<AimTurret>();
        }

        if(turrets == null || turrets.Count == 0)
        {
            Turret[] turrent = GetComponentsInChildren<Turret>();
            turrets = turrent.ToList<Turret>();
        }
    }

    public void HandleShoot()
    {
        if (canShoot)
        {
            foreach (var turret in turrets)
            {
                turret.Shoot();
            }
        }

    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        foreach(AimTurret turrent in aimTurret)
        {
            turrent.Aim(pointerPosition);
        }
    }
}
