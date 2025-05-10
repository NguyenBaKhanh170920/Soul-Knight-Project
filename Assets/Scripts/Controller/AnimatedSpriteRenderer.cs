using System.Collections;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;

    public float animationTime = 0.25f;
    private float timer;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;

    [Header("Flash when take damage")]
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;
    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;
    [SerializeField] private Material originalMaterial;

    // The currently running coroutine.
    private Coroutine flashRoutine;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
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

        if (loop && animationFrame >= animationSprites.Length)
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
    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.

        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }

}