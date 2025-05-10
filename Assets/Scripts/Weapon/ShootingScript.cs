using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEditor.Search;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gun;
    [SerializeField]
    private SpriteRenderer gunObject;
    Vector2 direction;
    public GameObject Bullet;
    public Transform shootPoint;
    public float BulletSpeed;
    [SerializeField]
    private BulletPooling BulletPooling;
    public AudioSource shootingSound;
    private bool isCooldown=false;
    public float cooldownTime = 1;
    void Start()
    {
        shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)gun.position;
        FaceMouse();
        if (Input.GetMouseButtonDown(0))
        {
            if (!isCooldown)
            {
                GameObject bullet = BulletPooling.Instance.GetBulletFromPool();
                if (bullet != null)
                {
                    Shoot(bullet);
                }
            }

        }
    }
    void FaceMouse()
    {
        gun.transform.right = direction;
        if (gun.rotation.z < -0.5f || gun.rotation.z > 0.5f)
        {
            gunObject.flipY = true;
        }
        else
        {
            gunObject.flipY = false;
        }
    }
    void Shoot(GameObject bullet)
    {
        if (bullet != null && !bullet.activeInHierarchy)
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * BulletSpeed);
            StartCoroutine(Cooldown());
            shootingSound.Play();
        }
    }
    //IEnumerator ResetBullet(GameObject bullet)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    bullet.SetActive(false);
    //}
    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
    public void ResetCooldown()
    {
        StopAllCoroutines();
        isCooldown = false;
    }
}
