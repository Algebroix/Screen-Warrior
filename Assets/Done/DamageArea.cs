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

    [SerializeField]
    private float cooldownDuration;

    private DamageAreaGraphics graphics;

    private int damageAreaLayer;

    private bool inUse = false;

    public void Cast()
    {
        //If already using, don't use
        if (!inUse)
        {
            StartCoroutine(Use());
        }
    }

    private IEnumerator Use()
    {
        //Lock using until done
        inUse = true;
        //Telegraph shows where the hit will land without dealing damage
        graphics.SetTelegraph();
        yield return new WaitForSeconds(telegraphDuration);
        //Set layer that results in dealing damage and change graphics accordingly
        gameObject.layer = damageAreaLayer;
        graphics.SetHit();
        yield return new WaitForSeconds(hitDuration);
        //Set layer back to default to not deal damage. Change graphics
        gameObject.layer = 0;
        graphics.SetInactive();
        yield return new WaitForSeconds(cooldownDuration);
        //Unlock using again
        inUse = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagableByEnemies damagable = collision.gameObject.GetComponent<IDamagableByEnemies>();
        //If object is damagable by enemies, damage it on collision
        if (damagable != null)
        {
            damagable.GetDamage(damage);
        }
    }

    private void Awake()
    {
        //Cache
        graphics = GetComponent<DamageAreaGraphics>();
        damageAreaLayer = LayerMask.NameToLayer("DamageArea");
    }
}