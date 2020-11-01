using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface used for polymorphism for controlling objects with different logic
public interface IControllable
{
    void SetControlled(bool value);

    float GetPower();

    void ResetPower();
}