using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for polymorphism of objects using territories in different ways
public interface ITerritoryUser
{
    void OnTerritoryEntered(Transform objectTransform);

    void OnTerritoryExited(Transform objectTransform);
}