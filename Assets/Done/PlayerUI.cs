using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Decoupled UI for easy exchange of placeholders
public class PlayerUI : MonoBehaviour
{
    private ScreenUI screenUI;
    private SpriteRenderer[] healthPoints;
    private PlayerController playerController;
    private int currentHp;

    private void SetHpBar()
    {
        int newHp = playerController.GetCurrentHp();
        //If player that hp has changed is focused on, update screen UI.
        if (playerController.IsFocus())
        {
            screenUI.SetHp(newHp);
        }

        //Update UI visible on player
        for (int healthPointIndex = Mathf.Min(currentHp, newHp); healthPointIndex < newHp; ++healthPointIndex)
        {
            healthPoints[healthPointIndex].enabled = true;
        }

        for (int healthPointIndex = newHp; healthPointIndex < Mathf.Max(currentHp, newHp); ++healthPointIndex)
        {
            healthPoints[healthPointIndex].enabled = false;
        }

        currentHp = newHp;
    }

    private void SetFocus()
    {
        //When focus in set on player, update screen UI with his HP
        screenUI.SetHp(playerController.GetCurrentHp());
    }

    private void Awake()
    {
        //Cache
        screenUI = FindObjectOfType<ScreenUI>();
        playerController = GetComponent<PlayerController>();
        healthPoints = GetComponentsInChildren<SpriteRenderer>();
        //Subscribe to player events
        playerController.onHpChanged += SetHpBar;
        playerController.onSetFocus += SetFocus;
    }
}