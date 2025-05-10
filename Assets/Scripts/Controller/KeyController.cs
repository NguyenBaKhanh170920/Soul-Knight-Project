using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
	[SerializeField]
	private GameObject _player;
	public float smoothTime;

	public bool IsPickedUp { get; set; } = false;

	private Vector2 _velocity;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (IsPickedUp)
		{
			transform.position = Vector2.SmoothDamp(transform.position, _player.transform.position, ref _velocity, smoothTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !IsPickedUp)
		{
			IsPickedUp = true;
		}
		//gameObject.GetComponent<Renderer>().enabled = false;
	}
}
