using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    //Setting to non zero value results in lives being at start above player count.
    [SerializeField]
    private int lives = 1;

    //Maximum number of lives. Must be set together with slots in UI.
    private int maxLives = 8;

    public event UnityAction onLivesChanged;

    public bool LooseLife()
    {
        //Decrease lives
        lives--;
        if (lives == 0)
        {
            //If no more lives, defeat
            Defeat();
            return false;
        }
        else
        {
            //Update observers
            onLivesChanged();
            return true;
        }
    }

    public void GainLife()
    {
        //Increase lives with cap at maxLives
        lives = Mathf.Min(lives + 1, maxLives);
        //Update observers
        onLivesChanged();
    }

    public int GetLives()
    {
        return lives;
    }

    private void Defeat()
    {
        //TODO:
        Debug.Log("Lost");
    }
}