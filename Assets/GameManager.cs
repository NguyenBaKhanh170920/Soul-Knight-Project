using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static int killedSpider = 0;
	[SerializeField]
	private GameObject spiderSpawner;
	[SerializeField]
	private GameObject key_2;

	public event EventHandler<GameObject> OnScene2Complete;

	// Start is called before the first frame update
	void Start()
	{
		OnScene2Complete += ActivateKey;
	}

	// Update is called once per frame
	void Update()
	{
		if (killedSpider == spiderSpawner.GetComponent<EnermySpawner>().maxSpawn)
		{
			OnScene2Complete(this, key_2);
		}

		if(Input.GetKeyDown(KeyCode.P)) {
			key_2?.SetActive(true);
		}
	}

	void ActivateKey(object sender, GameObject key)
	{
		key.SetActive(true);
	}
}
