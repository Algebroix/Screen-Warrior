using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float telegraphDuration;

    [SerializeField]
    private float hitDuration;

    private int damageAreaLayer;
    private Color hitColor;
    private Color telegraphColor;

    private SpriteRenderer spriteRenderer;

    private bool inUse = false;

    private void Telegraph()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.color = telegraphColor;
    }

    private void Hit()
    {
        gameObject.layer = damageAreaLayer;
        spriteRenderer.color = hitColor;
    }

    private void EndHit()
    {
        gameObject.layer = 0;
        spriteRenderer.enabled = false;
    }

    private IEnumerator Use()
    {
        inUse = true;
        Telegraph();
        yield return new WaitForSeconds(telegraphDuration);
        Hit();
        yield return new WaitForSeconds(hitDuration);
        EndHit();
        inUse = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagableByEnemies damagable = collision.gameObject.GetComponent<IDamagableByEnemies>();

        if (damagable != null)
        {
            damagable.GetDamage(damage);
        }
    }

    //TODO: remove
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Use());
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageAreaLayer = LayerMask.NameToLayer("DamageArea");

        hitColor = spriteRenderer.color;
        telegraphColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }
}