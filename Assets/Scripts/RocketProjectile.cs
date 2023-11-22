using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    [Header("Static Editor References")]
    [SerializeField] GameObject ProjectileObject = null;
    [SerializeField] GameObject DetonationObject = null;

    protected float DetonationLifeTime = -1991f;
    protected float DetonationMaxLifeTime = 1.0f;

    [Header("Dynamic Values")]
    [SerializeField] protected Vector3 spawnPos = Vector3.zero;
    [SerializeField] protected Vector3 aimPoint = Vector3.zero;

    protected Vector3 AimDirection = Vector3.zero;

    public void Start()
    {
        ProjectileObject = null;
        DetonationObject = null;
    }
    protected void Update()
    {
        if (DetonationObject != null && DetonationLifeTime > 0f)
        {
            DetonationLifeTime -= Time.deltaTime;
            if (DetonationLifeTime <= 0f)
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
        ProjectileObject.SetActive(false);

        if(DetonationObject != null)
        {
            DetonationObject.SetActive(false);
        }
    }

    public virtual void Init(Vector3 setSpawnPos, Vector3 setAimPoint)
    {
        spawnPos = setSpawnPos;
        aimPoint = setAimPoint;
        //|
        AimDirection = (aimPoint - spawnPos).normalized;
        transform.LookAt(aimPoint);
    }
}