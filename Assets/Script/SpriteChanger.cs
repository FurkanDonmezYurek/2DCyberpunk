using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;

    private bool isSprite1Active = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null && sprite1 != null)
        {
            spriteRenderer.sprite = sprite1;
        }
    }

    public void ChangeSprite()
    {
        if (isSprite1Active)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }
        isSprite1Active = !isSprite1Active;
    }
}
