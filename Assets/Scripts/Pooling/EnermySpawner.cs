using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawner : MonoBehaviour
{
	public GameObject objectToSpawn;
	public float spawnInterval = 3f;
	public float minSpawnRadius = 6f;
	public float maxSpawnRadius = 10f;
	public int poolSize = 10;
	private List<GameObject> pooledObjects = new List<GameObject>();
	public static int totalEnermyCreated = 0;
	public static int ObjectDestroyed = 0;
	[SerializeField] private LayerMask obstacleLayerMask;
	public int maxSpawn;
	private int monsterCount;

	// Start is called before the first frame update
	void Start()
	{
		InitializeObjectPool();
		StartCoroutine(SpawnObjectEverySecond());
	}

	void InitializeObjectPool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = Instantiate(objectToSpawn);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	IEnumerator<WaitForSeconds> SpawnObjectEverySecond()
	{
		while (monsterCount < maxSpawn)
		{
			SpawnObjectInRadius();
			yield return new WaitForSeconds(spawnInterval);
		}
	}

	void SpawnObjectInRadius()
	{
		while (true)
		{
			// Get the spawn point position
			Vector3 pivot = gameObject.transform.position;

			// Tạo một hướng ngẫu nhiên
			Vector3 randomDirection = Random.insideUnitCircle.normalized;

			// Tạo một khoảng cách ngẫu nhiên trong phạm vi quy định
			float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);

			// Tính toán vị trí spawn
			Vector3 spawnPosition = pivot + randomDirection * randomDistance;


			spawnPosition.z = 0f; // Điều chỉnh dựa trên setup 3D hoặc 2D của game

			if (IsInObstacleAre(spawnPosition))
			{
				continue;
			}
			// Lấy đối tượng từ pool
			GameObject obj = GetObjectFromPool();
			if (obj != null)
			{
				// Nếu đối tượng là enemy, đặt layerMask cho đối tượng
				obj.layer = 10;
				obj.transform.position = spawnPosition;

				// setup health bar cho enemy duoc spawn ra
				GameObject healthBar = HealthPool.SharedInstance.GetPooledObject();
				obj.GetComponent<BaseEnemy>().SetupHealthBar(healthBar);
				obj.SetActive(true);

				// increase monster count
				monsterCount++;
			}
			break;
		}
	}
	bool IsInObstacleAre(Vector3 position)
	{
		Collider2D collider = Physics2D.OverlapCircle(position, 2, obstacleLayerMask);
		return collider != null;
	}


	GameObject GetObjectFromPool()
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				totalEnermyCreated += 1;
				return pooledObjects[i];
			}
		}
		return null;
	}
}
