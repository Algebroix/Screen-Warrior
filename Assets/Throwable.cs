using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetJoint2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Throwable : MonoBehaviour, IControllable, IDamagableByEnemies
{
    [SerializeField]
    private float power;

    [SerializeField]
    private int maxHitPoints;

    [SerializeField]
    private bool magnetic;

    private Camera mainCamera;
    private TargetJoint2D targetJoint;
    private Rigidbody2D rigidBody;
    private Collider2D objectCollider;
    private SpriteRenderer spriteRenderer;
    private Transform throwableTransform;
    private int throwableLayer;
    private int inactiveLayer;
    private int rangeLayer;

    private Color startingColor;
    private int hitPoints;
    private bool active;

    public float GetPower()
    {
        return power;
    }

    public void ResetPower()
    {
    }

    public void SetControlled(bool value)
    {
        active = value;
        if (active)
        {
            StartCoroutine(Control());
            targetJoint.enabled = true;
        }
        else
        {
            targetJoint.enabled = false;
        }
    }

    private IEnumerator Control()
    {
        targetJoint.anchor = throwableTransform.InverseTransformPoint(objectCollider.ClosestPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition)));

        while (active)
        {
            targetJoint.target = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            yield return null;
        }
    }

    public void GetDamage(int damage)
    {
        hitPoints -= damage;

        spriteRenderer.color = startingColor * hitPoints / (float)maxHitPoints;

        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == rangeLayer && !magnetic)
        {
            gameObject.layer = throwableLayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == rangeLayer && !magnetic)
        {
            gameObject.layer = inactiveLayer;
            SetControlled(false);
        }
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        targetJoint = GetComponent<TargetJoint2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        throwableTransform = transform;
        startingColor = spriteRenderer.color;

        throwableLayer = LayerMask.NameToLayer("Throwable");
        inactiveLayer = LayerMask.NameToLayer("InactiveThrowable");
        rangeLayer = LayerMask.NameToLayer("ThrowRange");

        hitPoints = maxHitPoints;
    }
}