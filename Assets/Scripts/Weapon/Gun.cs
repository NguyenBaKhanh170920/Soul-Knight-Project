using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   
        {
            BaseEnemy enemy = collision.gameObject.GetComponentInChildren<BaseEnemy>();

            if (enemy != null)
            {
                enemy.GotDamage(damage);
            }
            gameObject.SetActive(false);
        }
        if(collision.gameObject.name == "Wall")
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Door")
        {
            gameObject.SetActive(false);
        }
    }


}
