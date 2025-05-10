using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
	[SerializeField]
	private ShopUI shop;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IShopCustomer customer = collision.GetComponent<IShopCustomer>();
		if (customer != null)
		{
			shop.Show(customer);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		IShopCustomer customer = collision.GetComponent<IShopCustomer>();
		if (customer != null)
		{
			shop.Hide();
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
