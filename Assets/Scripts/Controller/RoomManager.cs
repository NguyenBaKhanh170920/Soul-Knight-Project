using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> enemies;

    [SerializeField]
    private GameObject bossKey;

    [SerializeField]
    private GameObject spiderSpawner;

    public void EnemyDefeated(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            SpawnBossKey();
        }
    }

    public void ActivateSpawnSpider()
    {
        Debug.Log("mo cua");
        spiderSpawner.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnBossKey()
    {
        bossKey.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            bossKey.SetActive(true);
        }
    }
}
