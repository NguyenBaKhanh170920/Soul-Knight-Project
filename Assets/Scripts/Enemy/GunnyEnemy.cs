using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Pool;

public class GunnyEnemy : BaseEnemy
{
    private GameObject target;
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float speed = 2f;

    float _speed = 0f;

    [SerializeField]
    int fireRate = 2;

    float timeCount = 0f;
    private Canvas _canvas;
    new SpriteRenderer renderer;
    new Rigidbody2D rigidbody;
    Vector2 direction;


    new IEnumerator Start()
    {
        base.Start();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        // Wait until HealthPool is initialized
        while (HealthPool.SharedInstance == null)
        {
            yield return null; // Wait for next frame
        }
        GameObject healthBar = HealthPool.SharedInstance.GetPooledObject();
        gameObject.GetComponent<BaseEnemy>().SetupHealthBar(healthBar);
        gameObject.SetActive(true);
    }

    new void Update()
    {
        base.Update();
        timeCount += Time.deltaTime;
        if (this.target)
        {
            LookAtPlayer();
            DoIfSeePlayer();
        }
        if(_health <= 0)
        {
            _healthBar.SetActive(false);
            gameObject.SetActive(false);
        }
    }
    void LookAtPlayer()
    {
        direction = this.target.transform.position - transform.position;
        renderer.flipX = direction.x < 0;
        rigidbody.MovePosition(rigidbody.position + direction.normalized * _speed * Time.deltaTime);
    }

    void DoIfSeePlayer()
    {
        string[] layers = new string[] { "Player", "Obstacle" };
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, 50f, LayerMask.GetMask(layers));
        Color color = Color.blue;

        if (hit2D.collider && hit2D.collider.name.ToLower().Contains("player"))
        {
            color = Color.red;
            _speed = speed;
            if (timeCount * fireRate > 1)
            {
                timeCount = 0f;
                this.Fire();
            }
        }

        Debug.DrawRay(transform.position, direction, color);
    }

    void Fire()
    {
        if (bulletPrefab)
        {
            GameObject bullet = EnemyBulletPooling.Instance.GetBulletFromPool();
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
                bullet.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                //move to player direction
                bullet.transform.position = transform.position + (Vector3)direction.normalized;
            }
            //bullet = Instantiate(bulletPrefab, transform.position, Quaternion.FromToRotation(Vector2.up, direction));
            //bullet.SetActive(true);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _speed = 0;
    }
}
