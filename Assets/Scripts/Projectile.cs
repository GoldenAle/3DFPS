using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Static Editor References")]
    [SerializeField] protected GameObject projectileObject = null;
    [SerializeField] protected GameObject detonationObject = null;

    [Header("Static Values")]
    protected float movementSpeed = 20f;
    protected float maxLifetime = 20f;
    [SerializeField] protected float detonationMaxLifetime = 1f;

    [Header("Dynamic Values")]
    protected Vector3 spawnPos = Vector3.zero;
    protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    [SerializeField] protected float detonationLifetime = -1f;
    protected float lifetime = 0f;


    protected void Start()
    {
        projectileObject.SetActive(true);

        if (detonationObject != null)
        {
            detonationObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        HandleLifeTime();
        TryHandleDetonationLifetime();
    }

    private void HandleLifeTime()
    {
        lifetime += Time.deltaTime;
        if (lifetime > maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void TryHandleDetonationLifetime()
    {
        if (detonationObject != null && detonationLifetime > 0f)
        {
            detonationLifetime -= Time.deltaTime;
            if (detonationLifetime <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }

        projectileObject.SetActive(false);

        if (detonationObject != null)
        {
            detonationLifetime = detonationMaxLifetime;
            detonationObject.SetActive(true);
        }
    }

    public virtual void Init(Vector3 setSpawnPos, Vector3 setAimPoint)
    {
        spawnPos = setSpawnPos;
        transform.position = spawnPos;

        aimPoint = setAimPoint;
        aimDirection = (aimPoint - spawnPos).normalized;
        transform.LookAt(aimPoint);
    }
}