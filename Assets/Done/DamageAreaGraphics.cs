using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeholder graphics for damage areas
public class DamageAreaGraphics : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color hitColor;
    private Color telegraphColor;

    public void SetTelegraph()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.color = telegraphColor;
    }

    public void SetHit()
    {
        spriteRenderer.color = hitColor;
    }

    public void SetInactive()
    {
        spriteRenderer.enabled = false;
    }

    private void Awake()
    {
        //Cache
        spriteRenderer = GetComponent<SpriteRenderer>();

        hitColor = spriteRenderer.color;
        telegraphColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
    }
}