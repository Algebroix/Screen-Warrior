using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryCaster : MonoBehaviour, ITerritoryUser
{
    private DamageArea damageArea;

    public void OnTerritoryEntered(Transform objectTransform)
    {
        damageArea.Cast();
    }

    public void OnTerritoryExited(Transform objectTransform)
    {
    }

    private void Awake()
    {
        damageArea = GetComponentInChildren<DamageArea>();
    }
}