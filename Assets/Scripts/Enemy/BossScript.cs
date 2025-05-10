using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BossScript : BaseEnemy
{
    
    public GameObject target;
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float speed = 2f;

    float _speed = 0f;

    [SerializeField]
    float fireRate = 0.5f;

    [SerializeField]
    float damageRadius = 4f; // Damage radius for the skill

    float timeCount = 0f;
    bool isSkillActive = false; // Flag to prevent skill overlap
    [SerializeField]
    public GameObject endGameCanvas;
    new SpriteRenderer renderer;
    new Rigidbody2D rigidbody;
    Vector2 direction;
    [SerializeField]
    private GameObject redCirclePrefab;
    public BossAnimationRenderer bossAnimatedSpriteRenderer;

    public UnityEvent onBossDie;
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
        //scale health bar
        healthBar.transform.localScale = new Vector3(2, 2, 2);
        gameObject.GetComponent<BaseEnemy>().SetupHealthBar(healthBar);
        gameObject.SetActive(true);
    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Dungeon2");
    }
    public void Exit()
    {
        SceneManager.LoadScene("GameMenu");
    }
    // Update is called once per frame
    protected override void OnDie()
    {
        base.OnDie();
        Time.timeScale = 0;
        onBossDie?.Invoke();
        TimeController.finishTime = Time.time;
        Debug.Log(TimeController.finishTime);
    }
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
            Time.timeScale = 0;
            endGameCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void LookAtPlayer()
    {
        direction = this.target.transform.position - transform.position;
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
            if (timeCount * fireRate > 3)
            {
                timeCount = 0f;
                this.PerformSkill();
            }
            if(timeCount * fireRate > 2)
            {
                GameObject.Find("Player").GetComponent<Move>().speed = 5;
            }
        }

        Debug.DrawRay(transform.position, direction, color);
    }


    private void PerformSkill()
    {
        isSkillActive = true;
        // slide to player
        Fireball();
        CircleDead();
        // Disable movement
        _speed = 0;


        // Enable movement again
        _speed = speed;

        // End the skill
        isSkillActive = false;
    }
    // Fireball skill
    private void Fireball()
    {
        bossAnimatedSpriteRenderer.slash();
        if (bulletPrefab)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            if (bullet != null)
            {
                Vector3 offset = target.transform.position - transform.position;
                bullet.transform.position = transform.position + offset * 3;
                bullet.SetActive(true);

                bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
                bullet.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
                //move to player direction
                bullet.transform.position = transform.position + (Vector3)direction.normalized;
                //transform = player transform
                bullet.transform.parent = transform;
            }

        }
    }
    //Create red circle skill damage player 
    private void CircleDead()
    {
        // Instantiate the red circle and set its properties
        GameObject redCircle = Instantiate(redCirclePrefab);
        redCircle.transform.position = target.transform.position;
        //redCircle.transform.localScale = new Vector3(damageRadius * 2.1f, damageRadius * 2.1f, 1);

        // Start the coroutine to wait for 2 seconds
        StartCoroutine(Nothing(redCircle.transform.position));

        // Destroy the red circle after 1 second
        Destroy(redCircle, 1f);
    }

    private IEnumerator Nothing(Vector3 circlePosition)
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Check if any player is within the damage radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(circlePosition, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Start the coroutine to slow down the player
                StartCoroutine(SlowDownPlayer(hitCollider.gameObject.GetComponent<Move>()));
            }
        }
    }

    private IEnumerator SlowDownPlayer(Move playerMove)
    {
        float originalSpeed = playerMove.speed;
        playerMove.speed = originalSpeed / 2;
        yield return new WaitForSeconds(2f);
        playerMove.speed = originalSpeed;
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

    public enum SkillType
    {
        Fireball,
        CircleDead,
        SpanBullet
    }
}
