using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
	private Transform shopItemTemplate;
	private Transform container;
	private IShopCustomer shopCustomer;

	private void Awake()
	{
		container = transform.Find("container");
		shopItemTemplate = container.Find("shopItemTemplate");
		//shopItemTemplate.gameObject.SetActive(false);
	}

	private void CreateItemButton(Item.ItemType type, Sprite sprite, string itemName, int itemCost, int positionIndex)
	{
		Transform shopItemTrans = Instantiate(shopItemTemplate, container);
		RectTransform shopItemRectTransform = shopItemTrans.GetComponent<RectTransform>();
		float shopItemHeight = 150f;
		shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

		shopItemRectTransform.Find("txtPrice").GetComponent<Text>().text = itemCost.ToString();
		shopItemRectTransform.Find("txtName").GetComponent<Text>().text = itemName;
		shopItemRectTransform.Find("imgItem").GetComponent<Image>().sprite = sprite;

		shopItemTrans.GetComponent<Button_UI>().ClickFunc = () =>
		{
			// clicked on shop item
			TryBuyItem(type);
		};

	}

	private void TryBuyItem(Item.ItemType type)
	{
		if (shopCustomer.TrySpendGold(Item.GetCost(type)))
		{
			shopCustomer.BuyItem(type);

		}
	}

	public void Show(IShopCustomer customer)
	{
		this.shopCustomer = customer;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	// Start is called before the first frame update
	void Start()
	{
		CreateItemButton(Item.ItemType.HealthPotion, Item.GetSprite(Item.ItemType.HealthPotion), "Health Potion", Item.GetCost(Item.ItemType.HealthPotion), 0);
        CreateItemButton(Item.ItemType.UpgradeGun, Item.GetSprite(Item.ItemType.UpgradeGun), "Critical bullet", Item.GetCost(Item.ItemType.UpgradeGun), 1);

        Hide();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
