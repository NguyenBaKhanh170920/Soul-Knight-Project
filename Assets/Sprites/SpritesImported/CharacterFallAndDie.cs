using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFallAndDie : MonoBehaviour
{
    private Animator animator;
    public GameObject gameoverText;
    private Vector3 lastPosition;
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Start the destruction process
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        lastPosition = transform.position;
        // Destroy the GameObject
        Destroy(gameObject);
    }
}
