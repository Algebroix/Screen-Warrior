using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{
    private ITerritoryUser territoryUser;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        territoryUser.OnTerritoryEntered(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        territoryUser.OnTerritoryExited(collision.transform);
    }

    private void Awake()
    {
        territoryUser = GetComponentInParent<ITerritoryUser>();
    }
}