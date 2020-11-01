using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeOrb : MonoBehaviour
{
    private GameObject mergeObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Because of physics layer matrix every collision is from player layer which is used only for player
        so every colliding object is player*/

        if (mergeObject != null)
        {
            /*If some player is already colliding disable it, decrease number of players and disable orb.
            Number of lives doesn't change. This reserves life letting the player take care of less players*/
            mergeObject.SetActive(false);
            PlayersHolder.playerCount--;
            gameObject.SetActive(false);
        }
        else
        {
            //If no player is already colliding, save it for future collision.
            mergeObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If player exits collision, it can't be merged anymore.
        mergeObject = null;
    }
}