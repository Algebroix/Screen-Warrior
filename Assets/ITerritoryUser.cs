using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerritoryUser
{
    void OnTerritoryEntered(Transform objectTransform);

    void OnTerritoryExited(Transform objectTransform);
}