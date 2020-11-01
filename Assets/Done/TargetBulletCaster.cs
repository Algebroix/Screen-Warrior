using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBulletCaster : MonoBehaviour
{
    //Shoots at target transform to make targeting easier.
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float autoCastPeriod;

    [SerializeField]
    public float speed;

    [SerializeField]
    private BulletPool bulletPool;

    [SerializeField]
    private bool autoCast = false;

    private Transform casterTransform;
    private float currentTime;

    public void CastBullet()
    {
        //Get bullet from pool, reset, set velocity and enable
        Bullet bullet = bulletPool.GetBullet();
        bullet.transform.position = casterTransform.position;
        bullet.gameObject.SetActive(true);
        bullet.Cast(target.position - casterTransform.position, speed);
    }

    //Coroutine for shooting continously in set intervals
    private IEnumerator AutoCast()
    {
        while (autoCast)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= autoCastPeriod)
            {
                CastBullet();
                currentTime = 0.0f;
            }

            yield return null;
        }
    }

    public void SetAutocast(bool value)
    {
        autoCast = value;
        if (autoCast)
        {
            StartCoroutine(AutoCast());
        }
    }

    private void Awake()
    {
        //Cache
        casterTransform = transform;
        //TODO: Remove
        SetAutocast(true);
    }
}