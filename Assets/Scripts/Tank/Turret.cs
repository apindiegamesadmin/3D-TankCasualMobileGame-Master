using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    public List<Transform> turretBarrels;

    public TurretData turretData;

    private bool canShoot = true;
    private Collider2D[] tankColliders;
    private float currentDelay = 0;

    private ObjectPool bulletPool;
    [SerializeField]
    public int bulletPoolCount = 10;

    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReloading;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        bulletPool.Initialize(turretData.bulletPrefab, bulletPoolCount);
        OnReloading?.Invoke(currentDelay);
    }

    private void Update()
    {
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay/ turretData.reloadDelay);
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }

    /// <summary>
    /// Functionality for shooting
    /// </summary>
    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = turretData.reloadDelay;

            foreach (var barrel in turretBarrels)
            {
                var hit = Physics2D.Raycast(barrel.position, barrel.up);

                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);
                bullet.GetComponent<Bullet>().direction = barrel.up;

                foreach (var collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
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
