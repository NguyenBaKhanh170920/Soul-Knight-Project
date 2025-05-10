using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BulletScript : MonoBehaviour
{
    private float _damage = 2f;
    private new Rigidbody2D rigidbody;
    public float velocity = 2f;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = velocity * (this.transform.rotation * Vector3.up);
    } 

    public void SetDamage(float damage)
    {
        this._damage = damage;
    }
    public float GetDamage()
    {
        return this._damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Enemy"))
        {           
            if(collision.gameObject.name.Contains("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
                var healPlayer = collision.gameObject.GetComponent<PlayerController>().GetCurrentHealth();
                if (healPlayer <= 0)
                {
                    collision.gameObject.SetActive(false);
                }

            }
            gameObject.SetActive(false);
        }
        
    }
}

