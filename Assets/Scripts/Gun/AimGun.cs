using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
public class AimGun : MonoBehaviour
{
    public List<Transform> turretBarrels;
    public ParticleSystem muzzleFlash;

    public TurretData turretData;

    private bool canShoot = true;
    private Collider[] tankColliders;
    private float currentDelay = 0;

    private ObjectPool bulletPool;
    [SerializeField]
    public int bulletPoolCount = 10;

    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReloading;
    VariableJoystick aimJoyStick;
    void Start()
    {
        bulletPool = GetComponent<ObjectPool>();
        bulletPool.Initialize(turretData.bulletPrefab, bulletPoolCount);
        OnReloading?.Invoke(currentDelay);

        tankColliders = GetComponentsInParent<Collider>();
        aimJoyStick = GameObject.FindGameObjectWithTag("AimJoyStick").GetComponent<VariableJoystick>();
    }
    void Update()
    {
        float angle = Mathf.Atan2(aimJoyStick.Direction.x , aimJoyStick.Direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

        if(aimJoyStick.Shooting())
        {
            Shoot();
        }

        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay / turretData.reloadDelay);
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        Debug.Log("Shoot");
        if (canShoot)
        {
            canShoot = false;
            currentDelay = turretData.reloadDelay;

            foreach (var barrel in turretBarrels)
            {
                var hit = (Physics.Raycast(barrel.position, barrel.TransformDirection(Vector3.forward), Mathf.Infinity));

                muzzleFlash.Play();
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);
                bullet.GetComponent<Bullet>().direction = barrel.TransformDirection(Vector3.forward);

                foreach (var collider in tankColliders)
                {
                    Physics.IgnoreCollision(bullet.GetComponent<Collider>(), collider);
                }

            }

            OnShoot?.Invoke();
            OnReloading?.Invoke(currentDelay);
        }
        else
        {
            OnCantShoot?.Invoke();
        }

    }
}
