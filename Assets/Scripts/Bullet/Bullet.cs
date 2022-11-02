using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    [SerializeField] string targetTag;

    private Vector3 startPosition;
    private float conquaredDistance = 0;
    private Rigidbody rb;

    public UnityEvent OnHit = new UnityEvent();
    public Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        startPosition = transform.position;
        transform.GetComponentInChildren<TrailRenderer>().Clear();
        rb.velocity = transform.forward * this.bulletData.speed;
    }

    private void Update()
    {
        conquaredDistance = Vector3.Distance(transform.position, startPosition);
        if (conquaredDistance >= bulletData.maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit?.Invoke();
        if (other.transform.tag == targetTag)
        {
            var damagable = other.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.Hit(bulletData.damage);
            }
        }
        //DisableObject();
    }
}
