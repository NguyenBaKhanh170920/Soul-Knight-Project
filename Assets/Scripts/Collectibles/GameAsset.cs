using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
	private static GameAsset _instance;

	public Sprite healthPotion;
	public Sprite upgradeGun;

	public static GameAsset Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAsset>();
			}
			return _instance;
		}
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
