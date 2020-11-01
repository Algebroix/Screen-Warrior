using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrb : MonoBehaviour
{
    [SerializeField]
    private int healValue = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If colliding object is player and is not full hp, heal by set value and disable orb.
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null && !playerController.IsFullHp())
        {
            playerController.Heal(healValue);

            gameObject.SetActive(false);
        }
    }
}