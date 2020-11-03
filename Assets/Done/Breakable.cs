using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 3.0f;

    [SerializeField]
    private float invulnerableDuration = 1.0f;

    private float hp;

    //Decoupled graphics for easy exchange of placeholders
    private InvulnerabilityGraphics invulnerabilityGraphics;

    //Graphic change and bool lock for getting damage for set duration.
    private IEnumerator Invulnerable()
    {
        int originalLayer = gameObject.layer;
        invulnerabilityGraphics.SetInvulnerable();
        gameObject.layer = 0;

        yield return new WaitForSeconds(invulnerableDuration);

        gameObject.layer = originalLayer;
        invulnerabilityGraphics.SetStandard();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only controllable objects deal damage to Breakables.
        IControllable controllable = collision.GetComponent<IControllable>();
        if (controllable == null)
        {
            return;
        }

        float power = controllable.GetPower();
        //If power of hit is non-positive or Breakable is invulnerable, don't deal damage.

        //Decrease hp
        hp -= power;
        //If no hp left disable object.
        if (hp <= 0.0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            //if still alive make invulnerable for set time to avoid spamming attacks on breakable.
            StartCoroutine(Invulnerable());
        }

        //Reset attacking object power.
        controllable.ResetPower();
    }

    private void Awake()
    {
        //Initialize hp and cache.
        hp = maxHP;
        invulnerabilityGraphics = GetComponent<InvulnerabilityGraphics>();
    }
}