using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTarget : MonoBehaviour, ITarget
{
    private List<Transform> targets = new List<Transform>();

    public void AddTracked(Transform tracked)
    {
        targets.Add(tracked);
    }

    public Vector3 GetTarget()
    {
        return targets[targets.Count - 1].position;
    }

    public void RemoveTracked(Transform transform)
    {
        targets.Remove(transform);
    }
}