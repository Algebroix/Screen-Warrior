using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryCaster : MonoBehaviour, ITerritoryUser
{
    private DamageArea damageArea;

    public void OnTerritoryEntered(Transform objectTransform)
    {
        //When player entets territory cast damage area.
        damageArea.Cast();
    }

    public void OnTerritoryExited(Transform objectTransform)
    {
    }

    private void Awake()
    {
        //Cache
        damageArea = GetComponentInChildren<DamageArea>();
    }
}