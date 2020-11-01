using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for polymorphism that lets player and throwable objects be damaged by enemies using different logic.
public interface IDamagableByEnemies
{
    void GetDamage(int damage);
}