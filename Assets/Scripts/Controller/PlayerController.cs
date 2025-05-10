using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IShopCustomer
{
    [SerializeField]
    private GameObject _healthBar; // health bar prefab for player
    [SerializeField]
    private int _maxHealth;
    private int _currentHealth;
    public static int _currentGold = 0;          //Gold control
    public float dashBoost = 20; // lực khi người chơi lướt, + vào tốc độ di chuyển
    public float dashTime = 0.2f; // thời gian sử dụng kĩ năng lướt
    private float inDashTime; // tính toán thời gian lướt
    bool isDashing = false;  //kiểm tra đang lướt hay không
    private float damageInterval = 0.5f; // Variables for damage taken logic
    private bool isColliding = false;
    private Coroutine damageCoroutine;
    private bool canTakeDamage = true;
    private Move playerMovement;
    public float cooldownTime = 5f; // Time in seconds before the skill can be used again
    private bool isCooldown = false;
    public SkillCooldown skillCooldown;

    public AudioSource dashSound;
    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        playerMovement = GetComponent<Move>();
        SetupHealthBar();
    }

    private void SetupHealthBar()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        _healthBar = Instantiate(_healthBar, canvas.transform);
        _healthBar.name = "PlayerHP";
        _healthBar.GetComponent<HealthBarController>().Target = gameObject;
        _healthBar.GetComponent<HealthBarController>().Offset = new Vector3(0, 1, 0);

        _healthBar.GetComponent<HealthBarController>().SetHealth(_maxHealth, _maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))   //Cheat Code
        {
            Heal(5);
        }
        if (Input.GetKeyDown(KeyCode.Space) && inDashTime <= 0 && isDashing == false && isCooldown == false)
        {
            playerMovement.speed += dashBoost;
            inDashTime = dashTime;
            isDashing = true;
            StartCoroutine(Cooldown());
            dashSound.Play();
        }
        if (inDashTime <= 0 && isDashing == true)
        {
            playerMovement.speed -= dashBoost;
            isDashing = false;
        }
        else
        {
            inDashTime -= Time.deltaTime;
        }
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            //Load Scene game over
            SceneManager.LoadScene("GameOver");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage)
            return;
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        canTakeDamage = false;
        StartCoroutine(EnableDamageAfterInterval());
        _healthBar.GetComponent<HealthBarController>().SetHealth(_currentHealth, _maxHealth);
    }
    public void Heal(int healAmount)
    {
        int healthloss = _maxHealth - _currentHealth;
        if (healthloss < healAmount)
        {
            _currentHealth += healthloss;
        }
        else
        {
            _currentHealth += healAmount;
        }
        _healthBar.GetComponent<HealthBarController>().SetHealth(_currentHealth, _maxHealth);
    }
    public void AddGold(int goldAmount)
    {
        _currentGold += goldAmount;
    }


    private IEnumerator EnableDamageAfterInterval()
    {
        yield return new WaitForSeconds(damageInterval);
        canTakeDamage = true;
    }



    private void OnCollisionEnter2D(Collision2D collision)       //Collision and Damage Taken in a 2 second interval using isColliding bool and Coroutine.
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isColliding)
            {
                isColliding = true;
                damageCoroutine = StartCoroutine(TakeDamageOverTime());
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isColliding)
            {
                isColliding = false;
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }
            }
        }
    }
    private IEnumerator TakeDamageOverTime()
    {
        while (isColliding)
        {
            if (canTakeDamage)
            {
                TakeDamage(1);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void AddHealthPotiton()
    {
        Heal(2);

    }

    public void BuyItem(Item.ItemType type)
    {
        Debug.Log("Bought item: " + type);
        switch (type)
        {
            case Item.ItemType.HealthPotion:
                AddHealthPotiton();
                break;
            case Item.ItemType.UpgradeGun:
                UpgradeGun();
                break;
        }
    }

    public void UpgradeGun()
    {
        Debug.Log("Upgrade gun");
        BulletPooling pooling = FindObjectOfType<BulletPooling>();
        pooling.SetDamageBullet(50);
        pooling.SetBulletColor(new Color32(231, 255, 95, 255));
    }

    public bool TrySpendGold(int amount)
    {
        if (_currentGold >= amount)
        {
            _currentGold -= amount;
            //OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Cooldown()
    {
        skillCooldown.cooldown = cooldownTime;
        skillCooldown.enabled = true;
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
        skillCooldown.enabled = false;
    }
}
