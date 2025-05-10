using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwordScript : MonoBehaviour
{
    public Transform sword;
    [SerializeField]
    private SpriteRenderer swordObject;
    Vector2 direction;
    private bool slashing;
    private bool isCooldown = false;
    private Animator animator;
    public AudioSource swordSlash;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator = gameObject.GetComponent<Animator>();
        swordSlash = GetComponent<AudioSource>();
        slashing = false;
    }
    void FaceMouse()
    {
        sword.transform.right = direction;
        if (sword.rotation.z < -0.5f || sword.rotation.z > 0.5f)
        {
            swordObject.flipY = true;
        }
        else
        {
            swordObject.flipY = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)sword.position;
        FaceMouse();
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }
    public void Attack()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        slashing = true;
        swordSlash.Play();
        animator.SetBool("isAttack", slashing);
    }
    public void EndAttack()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        slashing = false;
        animator.SetBool("isAttack", slashing);
    }
    public void TurnOfCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy" && slashing)
    //    {
    //        BaseEnemy enemy = collision.gameObject.GetComponentInChildren<BaseEnemy>();
    //        enemy.GotDamage(4);
    //    }


    //}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && slashing)
        {
            BaseEnemy enemy = collision.gameObject.GetComponentInChildren<BaseEnemy>();
            enemy.GotDamage(4);
        }
    }
    public void ResetCooldown()
    {
        StopAllCoroutines();
        isCooldown = false;
    }


}
