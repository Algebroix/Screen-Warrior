using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface ITarget
{
    void AddTracked(Transform transform);

    void RemoveTracked(Transform transform);

    Vector3 GetTarget();
}