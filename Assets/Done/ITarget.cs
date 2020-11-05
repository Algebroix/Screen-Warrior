using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//Used for polymorphism for different types of targets
public interface ITarget
{
    void AddTracked(Transform transform);

    void RemoveTracked(Transform transform);

    Vector3 GetTarget();
}