using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTarget : MonoBehaviour, ITarget
{
    //Tracks previous targets to be able to set it when newer one is removed
    private List<Transform> targets = new List<Transform>();

    public void AddTracked(Transform tracked)
    {
        targets.Add(tracked);
    }

    public Vector3 GetTarget()
    {
        //Always shoot last target
        return targets[targets.Count - 1].position;
    }

    public void RemoveTracked(Transform transform)
    {
        targets.Remove(transform);
    }
}