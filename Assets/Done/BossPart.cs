using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPart : MonoBehaviour
{
    [SerializeField]
    private float invulnerableDuration = 1.0f;

    [SerializeField]
    private float damageMultiplier = 1.0f;

    private Boss boss;

    //Decoupled graphics for easy exchange of placeholders
    private InvulnerabilityGraphics invulnerabilityGraphics;

    private bool invulnerable;

    //Graphic change and bool lock for getting damage for set duration
    private IEnumerator Invulnerable()
    {
        invulnerabilityGraphics.SetInvulnerable();
        invulnerable = true;

        yield return new WaitForSeconds(invulnerableDuration);

        invulnerable = false;
        invulnerabilityGraphics.SetStandard();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Only controllable objects deal damage to Boss parts.
        IControllable controllable = collision.collider.GetComponent<IControllable>();
        if (controllable == null)
        {
            return;
        }

        float power = controllable.GetPower();
        //If power of hit is non-positive or Boss part is invulnerable, don't deal damage.
        if (!invulnerable && power > 0.0f)
        {
            //Pass damage to Boss.
            boss.GetDamage(power * damageMultiplier);
            //Become Invulnerable for set duration to avoid spamming attacks on single part.
            StartCoroutine(Invulnerable());
        }
        //Reset attacking object power.
        controllable.ResetPower();
    }

    private void Awake()
    {
        //Cache
        boss = GetComponentInParent<Boss>();
        invulnerabilityGraphics = GetComponentInParent<InvulnerabilityGraphics>();
    }
}