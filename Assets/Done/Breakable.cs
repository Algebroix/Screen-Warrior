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

    private bool invulnerable;

    //Graphic change and bool lock for getting damage for set duration.
    private IEnumerator Invulnerable()
    {
        invulnerabilityGraphics.SetInvulnerable();
        invulnerable = true;

        yield return new WaitForSeconds(invulnerableDuration);

        invulnerable = false;
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
        if (!invulnerable && power > 0.0f)
        {
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
    }

    private void Awake()
    {
        //Initialize hp and cache.
        hp = maxHP;
        invulnerabilityGraphics = GetComponent<InvulnerabilityGraphics>();
    }
}