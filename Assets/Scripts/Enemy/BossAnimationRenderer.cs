using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationRenderer : MonoBehaviour
{
    [Header("Sprites")]
    public BossAnimatedSpriteRenderer spriteRendererSlash;
    public BossAnimatedSpriteRenderer spriteRendererDie;
    public BossAnimatedSpriteRenderer spriteRendererTakeHit;
    //public AnimatedSpriteRenderer spriteRendererCast;
    private BossAnimatedSpriteRenderer activeSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    public AudioSource screamSound;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.K))
    //    {
    //        //slash();
    //    }
    //    else if (Input.GetKey(KeyCode.J))
    //    {
    //        spriteRenderer.enabled = false;
    //        die();
    //    }
    //    else if (Input.GetKey(KeyCode.M))
    //    {
    //        spriteRenderer.enabled = false;
    //        //TakeHit();
    //    }
    //}
    //private void SetDirection(BossAnimatedSpriteRenderer spriteRenderer)
    //{
    //    spriteRendererSlash.enabled = spriteRenderer == spriteRendererSlash;
    //    spriteRendererDie.enabled = spriteRenderer == spriteRendererDie;
    //    spriteRendererTakeHit.enabled = spriteRenderer == spriteRendererTakeHit;
    //    activeSpriteRenderer = spriteRenderer;
    //}
    public void slash()
    {
        screamSound.Play();
        spriteRenderer.enabled = false;
        spriteRendererSlash.enabled = true;
    }
    public void die()
    {
        spriteRenderer.enabled = false;
        spriteRendererDie.enabled = true;
    }
    public void TakeHit()
    {
        spriteRenderer.enabled = false;
        spriteRendererTakeHit.enabled = true;
    }
}
