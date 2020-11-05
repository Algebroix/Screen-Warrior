using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleScoreObject : MonoBehaviour
{
    [SerializeField]
    private int score;

    private ScreenUI screenUI;

    private void OnDisable()
    {
        //When object is disabled, add its score to player score
        screenUI.UpdateScore(score);
    }

    private void Awake()
    {
        //Cache
        screenUI = FindObjectOfType<ScreenUI>();
    }
}