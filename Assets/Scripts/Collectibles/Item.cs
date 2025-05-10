using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        HealthPotion,
        Gun,
        UpgradeGun
    }

    public static int GetCost(ItemType type)
    {
        switch (type)
        {
            case ItemType.HealthPotion: return 1;
            case ItemType.Gun: return 2;
            case ItemType.UpgradeGun: return 20;
            default: return 0;
        }
    }

    public static Sprite GetSprite(ItemType type)
    {
        switch (type)
        {
            case ItemType.HealthPotion:
                return GameAsset.Instance.healthPotion;
            case ItemType.UpgradeGun:
                return GameAsset.Instance.upgradeGun;
            default:
                return null;
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
