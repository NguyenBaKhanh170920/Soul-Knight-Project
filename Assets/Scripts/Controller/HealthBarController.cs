using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
	private Image _health; // Image Component of Health GO (child GO of Health BG)

	private Vector3 _offset;
	private GameObject _target;

	public Vector3 Offset
	{
		get { return _offset; }
		set { _offset = value; }
	}

	public GameObject Target
	{
		get { return _target; }
		set { _target = value; }
	}

	public void SetHealth(int health, int maxHeath)
	{
		//_health.GetComponent<Image>().fillAmount = health * 1.0f / maxHeath;
		_health = transform.Find("Health").GetComponent<Image>();
		_health.fillAmount = health * 1.0f / maxHeath;
	}

	// Start is called before the first frame update
	void Start()
	{
		_health = transform.Find("Health").GetComponent<Image>();
		_health.fillAmount = 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (Target.name == "Player")
			return;
		transform.position = Camera.main.WorldToScreenPoint(_target.transform.position + _offset);
	}
}
