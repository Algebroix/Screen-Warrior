using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHolder : MonoBehaviour
{
    private static PlayerController[] players;

    public static int playerCount = 0;

    public PlayerController GetInactivePlayer()
    {
        //Find first inactive player in cached players and return it
        for (int playerIndex = 0; playerIndex < players.Length; playerIndex++)
        {
            if (!players[playerIndex].isActiveAndEnabled)
            {
                return players[playerIndex];
            }
        }
        //If all players are already active return nothing
        return null;
    }

    /*Function callable from anywhere that resets physical tension for all players. It is needed for collisions between players
    to be stable when the player leaves control when there are conflicting tension forces working.*/

    public static void RemoveTension()
    {
        foreach (var player in players)
        {
            player.RemoveTension();
        }
    }

    private void Awake()
    {
        //Max number of active players is defined by player count in editor
        players = GetComponentsInChildren<PlayerController>(true);
    }
}