using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour
{
	public static HealthPool SharedInstance;
	private List<GameObject> _pooledObjects;
	public GameObject objectToPool; // HealthBar prefabs (assigned in Inspector)
	public int amountToPool;

	private Canvas _canvas;

	private void Awake()
	{
		SharedInstance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		_canvas = GameObject.FindObjectOfType<Canvas>();

		_pooledObjects = new List<GameObject>();
		GameObject tmp;
		for (int i = 0; i < amountToPool; i++)
		{
			tmp = Instantiate(objectToPool, _canvas.transform);
			tmp.SetActive(false);
			_pooledObjects.Add(tmp);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < amountToPool; i++)
		{
			if (!_pooledObjects[i].activeInHierarchy)
			{
				return _pooledObjects[i];
			}
		}

		return null;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
