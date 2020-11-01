using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrb : MonoBehaviour
{
    private PlayersHolder playersHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get new player from pool
        PlayerController playerController = playersHolder.GetInactivePlayer();

        if (playerController != null)
        {
            //If max value of active players was not yet reached activate it at orb's position and disable orb.
            playerController.transform.position = transform.position;
            playerController.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        //Cache
        playersHolder = FindObjectOfType<PlayersHolder>();
    }
}