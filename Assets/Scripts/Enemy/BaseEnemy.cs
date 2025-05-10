using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class BaseEnemy : MonoBehaviour
{
    //public Player target;

    public int maxHealth = 10;
    [SerializeField]
    protected int _health; // current health
    [SerializeField]
    private Vector3 healthBarOffset;
    protected GameObject _healthBar;

    public UnityEvent<GameObject> OnEnemyDie;

    public float Health
    {
        get { return _health; }
    }

    // Use this for initialization
    protected void Start()
    {
        _health = maxHealth;
    }

    public void SetupHealthBar(GameObject healthBar)
    {
        _healthBar = healthBar;
        _healthBar.SetActive(true);
        _healthBar.GetComponent<HealthBarController>().Target = gameObject;
        _healthBar.GetComponent<HealthBarController>().Offset = healthBarOffset;
        _health = maxHealth;
        _healthBar.GetComponent<HealthBarController>().SetHealth(_health, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collider2D collider = collision;
        //Debug.Log("Dungeon: collision " + collider);
        //if (collider.tag == TAG.SWORD)
        //{
        //    SwordAttack swordAttack = collider.GetComponent<SwordAttack>();
        //    if (swordAttack.attacking)
        //    {
        //        Debug.Log("Attacked" + swordAttack.baseWeapon.damage);
        //        swordAttack.attacking = false;
        //        this.GotDamage(swordAttack.baseWeapon.damage, collider);
        //    }
        //}
        //else if (collider.tag == TAG.BULLET)
        //{
        //    Bullet bullet = collider.GetComponent<Bullet>();
        //    if (bullet != null)
        //    {
        //        Debug.Log("Attacked" + bullet.GetDamage());
        //        this.GotDamage(bullet.GetDamage(), collider);
        //    }
        //}
    }

    public virtual void GotDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            this.OnDie();
        }
        _healthBar.GetComponent<HealthBarController>().SetHealth(_health, maxHealth);
    }

    protected virtual void OnAttacked(Collision2D collision)
    {
        // 
    }

    protected virtual void OnDie()
    {
        ParametersScript.scoreValue += 10;
        gameObject.SetActive(false);
        _healthBar.SetActive(false);
        OnEnemyDie?.Invoke(gameObject);

        int score = PlayerPrefs.GetInt("BestScore", 0);
        Debug.Log("Score: " + ParametersScript.scoreValue);
        if (ParametersScript.scoreValue > score)
        {
            PlayerPrefs.SetInt("BestScore", ParametersScript.scoreValue);
        }
        //Debug.Log($"Point: {ParametersScript.scoreValue}");
    }

    // Update is called once per frame
    protected void Update()
    {
    }
}
