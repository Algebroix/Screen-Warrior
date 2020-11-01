using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[RequireComponent(typeof(TargetJoint2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour, IControllable, IDamagableByEnemies
{
    //Power that is setting when not moving
    [SerializeField]
    private float standardPower = 0.01f;

    //Velocity threshold at which power resets
    [SerializeField]
    private float powerResetThreshold = 0.02f;

    //Rate at which power increases
    [SerializeField]
    private float powerIncrease = 0.05f;

    //Maximum power
    [SerializeField]
    private float maxPower = 1.0f;

    //Rate at which power is reducing when turning
    [SerializeField]
    private float reduceOnTurnRate = 100.0f;

    //How long player is invulnerable after taking damage
    [SerializeField]
    private float invulnerableDuration = 0.5f;

    //Events used to update UI
    public event UnityAction onHpChanged;

    public event UnityAction onSetFocus;

    private bool controlled = false;
    private bool focus = false;
    private bool invulnerable = false;

    private Camera mainCamera;
    private TargetJoint2D targetJoint;
    private SpriteRenderer spriteRenderer;
    private Life life;
    private GameObject throwRange;
    private Transform playerTransform;

    private Vector2 lastVelocity;
    private Vector2 lastPosition;
    private Color noFocusColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color focusColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color invulnerableColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);

    private float currentPower;
    private int currentHp;
    private int startingHp = 5;
    public static int maxHp = 5;

    private void OnSetFocus()
    {
        throwRange.SetActive(true);
        SetColor(focusColor);
    }

    public int GetCurrentHp()
    {
        return currentHp;
    }

    public void GetDamage(int damage)
    {
        if (!invulnerable)
        {
            currentHp = Mathf.Max(currentHp - damage, 0);

            onHpChanged();

            if (currentHp == 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Invulnerable());
            }
        }
    }

    private IEnumerator Invulnerable()
    {
        invulnerable = true;
        currentPower = 0.0f;
        SetColor(invulnerableColor);
        yield return new WaitForSeconds(invulnerableDuration);
        ResetColor();
        invulnerable = false;
    }

    private void Die()
    {
        bool alive = life.LooseLife();
        if (!alive || PlayersHolder.playerCount > 1)
        {
            gameObject.SetActive(false);
            PlayersHolder.playerCount--;
        }
        else
        {
            ResetPlayer();
        }
    }

    public void Heal(int hp)
    {
        currentHp = Mathf.Min(currentHp + hp, maxHp);
        onHpChanged();
    }

    public bool IsFullHp()
    {
        return currentHp == maxHp;
    }

    private void SetColor(Color color)
    {
        if (!invulnerable)
        {
            spriteRenderer.color = color;
        }
        else
        {
            spriteRenderer.color = invulnerableColor;
        }
    }

    private void ResetColor()
    {
        if (focus)
        {
            spriteRenderer.color = focusColor;
        }
        else
        {
            spriteRenderer.color = noFocusColor;
        }
    }

    public void SetControlled(bool value)
    {
        controlled = value;

        if (controlled)
        {
            StartCoroutine(Control());
            SetFocus();
        }

        ResetPower();
    }

    public void SetFocus()
    {
        focus = true;

        onSetFocus();
    }

    public void LooseFocus()
    {
        focus = false;
        throwRange.SetActive(false);
        SetColor(noFocusColor);
    }

    public bool IsFocus()
    {
        return focus;
    }

    public void ResetPower()
    {
        currentPower = standardPower;
        SetColor(focusColor);
    }

    public bool IsActive()
    {
        return controlled;
    }

    public float GetPower()
    {
        return currentPower;
    }

    public void ResetPlayer()
    {
        currentHp = startingHp;
        currentPower = standardPower;
        lastVelocity = VectorUtility.zero;
        lastPosition = VectorUtility.zero;

        onHpChanged();
    }

    public void InitPlayer()
    {
        ResetPlayer();
        PlayersHolder.playerCount++;
        life.GainLife();
    }

    private IEnumerator Control()
    {
        while (controlled)
        {
            Vector2 currentPosition = playerTransform.position;
            Vector2 currentVelocity = currentPosition - lastPosition;

            if (!invulnerable)
            {
                if (currentVelocity.magnitude < powerResetThreshold)
                {
                    currentPower = standardPower;
                }
                else
                {
                    currentPower += powerIncrease * currentVelocity.magnitude * (1.0f - reduceOnTurnRate / currentVelocity.magnitude / currentVelocity.magnitude * (1.0f - Vector2.Dot(lastVelocity.normalized, currentVelocity.normalized)));
                    currentPower = Mathf.Clamp(currentPower, 0.0f, maxPower);
                }
            }

            targetJoint.target = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            lastVelocity = currentVelocity;
            lastPosition = currentPosition;

            //using color scale white->yellow->red
            Color powerColor;
            if (currentPower < 0.5f * maxPower)
            {
                powerColor = Color.Lerp(Color.white, Color.yellow, currentPower / maxPower * 2.0f);
            }
            else
            {
                powerColor = Color.Lerp(Color.yellow, Color.red, currentPower / maxPower * 2.0f - 1.0f);
            }
            SetColor(powerColor);

            yield return null;
        }

        PlayersHolder.RemoveTension();
    }

    public void RemoveTension()
    {
        if (targetJoint != null)
        {
            targetJoint.target = playerTransform.position;
        }
    }

    private void OnEnable()
    {
        InitPlayer();
    }

    private void Awake()
    {
        onSetFocus += OnSetFocus;
        life = FindObjectOfType<Life>();
        throwRange = GetComponentsInChildren<Collider2D>(true)[1].gameObject;
        targetJoint = GetComponent<TargetJoint2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = transform;
        mainCamera = Camera.main;
    }
}