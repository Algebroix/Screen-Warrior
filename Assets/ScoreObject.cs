using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    [SerializeField]
    private int score;

    private ScreenUI screenUI;

    private void OnDisable()
    {
        screenUI.UpdateScore(score);
    }

    private void Awake()
    {
        screenUI = FindObjectOfType<ScreenUI>();
    }
}