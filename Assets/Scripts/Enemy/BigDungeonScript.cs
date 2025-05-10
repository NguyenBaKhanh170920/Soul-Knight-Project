using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDungeonScript : BaseEnemy
{
    private GameObject target;
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float speed = 2f;

    float _speed = 0f;

    [SerializeField]
    float fireRate = 0.5f;

    [SerializeField]
    float damageRadius = 2f; // Damage radius for the skill
    [SerializeField]
    int damageAmount = 1; // Damage amount for the skill

    float timeCount = 0f;
    bool isSkillActive = false; // Flag to prevent skill overlap
    private Canvas _canvas;
    new SpriteRenderer renderer;
    new Rigidbody2D rigidbody;
    Vector2 direction;
    [SerializeField]
    private GameObject lightningPrefab;

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

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        timeCount += Time.deltaTime;
        if (this.target && !isSkillActive) // Prevent movement and attack during skill
        {
            LookAtPlayer();
            DoIfSeePlayer();
        }
        if (_health <= 0)
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
            }
        }

        Debug.DrawRay(transform.position, direction, color);
    }


    private void PerformSkill()
    {
        isSkillActive = true;
        // slide to player

        // Disable movement
        _speed = 0;
        //tạo vòng tròn màu đỏ xung quanh enemy
        GameObject lightning = Instantiate(lightningPrefab, gameObject.transform);
        //lightning.transform.localPosition = new Vector3(0,3,0);
        lightning.transform.localRotation = Quaternion.identity;
        lightning.transform.localScale = new Vector3(3f, 3f, 1);
        lightning.layer = 10;

        // Create a spherecast to detect nearby players
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Apply damage to player
                hitCollider.gameObject.GetComponent<PlayerController>().TakeDamage(damageAmount); // Ensure Player script has a TakeDamage method
            }
        }
        Destroy(lightning, 1.5f);
        // Enable movement again
        _speed = speed;

        // End the skill
        isSkillActive = false;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        _speed = 0;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw damage radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
    public override void GotDamage(int damage)
    {
        base.GotDamage(damage);
        PerformSkill();
    }

}
