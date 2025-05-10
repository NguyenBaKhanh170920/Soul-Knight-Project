using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
	public bool locked;

	[SerializeField]
	private GameObject key;

	[SerializeField]
	private string sceneName;

	private Animator _animator;

	public UnityEvent OnDoorOpen;

	public AudioSource openDoorSound;
	public AudioSource closeDoorSound;

	// Start is called before the first frame update
	void Start()
	{
		locked = true;
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == key && locked) // need to change later
		{
            //openDoorSound.Play();
            _animator.SetTrigger("Open");
			locked = false;
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
			OnDoorOpen?.Invoke();
			Debug.Log("Scene name : " + sceneName);
			if (!string.IsNullOrEmpty(sceneName))
			{

				SceneManager.LoadScene(sceneName);
            }
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == key && !locked)
		{
			//closeDoorSound.Play();	
			_animator.SetTrigger("Close");
			locked = true;
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			collision.gameObject.SetActive(false);
		}
	}
}
