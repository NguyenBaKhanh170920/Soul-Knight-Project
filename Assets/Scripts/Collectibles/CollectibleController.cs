using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public enum CollectibleType { Coin, HealthPotion, BagOfCoin }
    public CollectibleType type;
    private int healAmount = 3;  // Amount of health the potion will heal
    private int coinValue = 1;    // Amount of gold the coin will add
    private int coinBagValue = 10;    // Amount of gold the coin will add
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        PlayerController stats = player.GetComponent<PlayerController>();
        if (stats != null)
        {
            switch (type)
            {
                case CollectibleType.Coin:
                    stats.AddGold(coinValue);
                    break;
                case CollectibleType.HealthPotion:
                    stats.Heal(healAmount);
                    break;
                case CollectibleType.BagOfCoin:
                    stats.AddGold(coinBagValue);
                    break;

            }
        }
        else
        {
            Debug.Log("Unfound referenced script");
        }
        Destroy(gameObject);
    }
}
