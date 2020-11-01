using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreContainer;

    [SerializeField]
    private GameObject hpContainer;

    [SerializeField]
    private GameObject lifeContainer;

    private Life life;

    private Image[] healthPoints;
    private Image[] lifePoints;
    private int score;
    private int hp = 0;
    private int lives = 0;
    private int maxHpBars;

    public void UpdateScore(int change)
    {
        score += change;
        scoreContainer.text = score.ToString();
    }

    public void SetHp(int value)
    {
        value = Mathf.Clamp(value - 1, 0, maxHpBars);
        for (int healthPointIndex = Mathf.Min(hp, value); healthPointIndex < value; ++healthPointIndex)
        {
            healthPoints[healthPointIndex].enabled = true;
        }

        for (int healthPointIndex = value; healthPointIndex < Mathf.Max(hp, value); ++healthPointIndex)
        {
            healthPoints[healthPointIndex].enabled = false;
        }

        hp = value;
    }

    public void SetLives()
    {
        int newLives = life.GetLives() - 1;
        for (int lifePointIndex = Mathf.Min(lives, newLives); lifePointIndex < newLives; ++lifePointIndex)
        {
            lifePoints[lifePointIndex].enabled = true;
        }

        for (int lifePointIndex = newLives; lifePointIndex < Mathf.Max(lives, newLives); ++lifePointIndex)
        {
            lifePoints[lifePointIndex].enabled = false;
        }

        lives = newLives;
    }

    private void Awake()
    {
        life = FindObjectOfType<Life>();
        life.onLivesChanged += SetLives;
        healthPoints = hpContainer.GetComponentsInChildren<Image>(true);
        lifePoints = lifeContainer.GetComponentsInChildren<Image>(true);
        lives = lifePoints.Length;
        maxHpBars = healthPoints.Length;
    }
}