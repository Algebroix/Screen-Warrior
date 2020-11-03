using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBulletCaster : MonoBehaviour, ITerritoryUser
{
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

    private int playersInTerritory = 0;

    //Shoots at target transform to make targeting easier.
    private ITarget target;

    public void CastBullet()
    {
        //Get bullet from pool, reset, set velocity and enable
        Bullet bullet = bulletPool.GetBullet();
        bullet.transform.position = casterTransform.position;
        bullet.gameObject.SetActive(true);
        bullet.Cast(target.GetTarget() - casterTransform.position, speed);
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

    //If territory is defined for this caster then it passes object changes in territory to target and starts autocasting
    public void OnTerritoryEntered(Transform objectTransform)
    {
        playersInTerritory++;
        target.AddTracked(objectTransform);
        SetAutocast(true);
    }

    /*If territory is defined for this caster then it passes object changes in territory to target,
    and if there are no players in territory stops autocasting*/

    public void OnTerritoryExited(Transform objectTransform)
    {
        playersInTerritory--;
        target.RemoveTracked(objectTransform);
        if (playersInTerritory == 0)
        {
            SetAutocast(false);
        }
    }

    private void Awake()
    {
        //Cache
        casterTransform = transform;
        target = GetComponentInChildren<ITarget>();
    }
}