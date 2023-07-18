using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    Animator animator;
    Image image;
    SpriteRenderer spriteRenderer;

    private void LateUpdate()
    {
        image.sprite = spriteRenderer.sprite;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    public void PlayHitAnimation()
    {
        animator.SetTrigger("Hit");
    }
}
