using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class BossAnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;
    public GameObject boss;

    public float animationTime = 0.25f;
    private float timer;
    private int animationFrame;

    public bool loop = true;
    public bool idle ;
    private int count = 0;


    // The currently running coroutine.
    private Coroutine flashRoutine;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        count = 0;
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        idle = false;
        boss.GetComponent<SpriteRenderer>().enabled = true;
        spriteRenderer.enabled = false;

    }

    private void Start()
    {
        boss = GameObject.Find("demon_cleave");
        StartCoroutine(AnimationCoroutine());
        //InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }
    private IEnumerator AnimationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(animationTime);
            NextFrame();
        }
    }

    private void NextFrame()
    {
        animationFrame++;
        if(animationFrame >= animationSprites.Length)
        {
            count++;
            idle=true;
            animationFrame = 0;
            this.enabled = false;
        }
        if (loop && count!=0)
        {
            animationFrame = 0;
        }

        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }

    internal void slash()
    {
        throw new NotImplementedException();
    }
}
