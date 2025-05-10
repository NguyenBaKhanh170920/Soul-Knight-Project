using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling Instance;
    public GameObject bulletPrefab;
    public int poolSize = 3;
    private List<GameObject> bulletPool;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public GameObject GetBulletFromPool()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        return null;
    }

    public void SetDamageBullet(int damage)
    {
        foreach (GameObject bullet in bulletPool)
        {
            Bullet bul = bullet?.GetComponent<Bullet>();
            if(bul != null)
            {
                bul.damage = damage;
            }
        }
    }

    public void SetBulletColor(Color32 color)
    {
        foreach (GameObject bullet in bulletPool)
        {
            Bullet bul = bullet?.GetComponent<Bullet>();
            if (bul != null)
            {
                bul.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
