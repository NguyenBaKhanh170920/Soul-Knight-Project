using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooling : MonoBehaviour
{
    public static EnemyBulletPooling Instance;
    public GameObject bulletPrefab;
    public int poolSize = 3;
    private List<GameObject> bulletPool;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializePool();
    }
    void InitializePool()
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

}
