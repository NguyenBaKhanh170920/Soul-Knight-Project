using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : BaseEnemy
{
    public GameObject droppedHealthPotion;
    public GameObject droppedCoin;
    public GameObject droppedBagOfCoin;

    protected override void OnDie()
    {
        base.OnDie();
        GameManager.killedSpider++;

        if (Random.value < 0.1f)
        {
            Instantiate(droppedBagOfCoin, transform.position, Quaternion.identity);
        }
        else

            if (Random.value > 0.9f)
        {
            Instantiate(droppedHealthPotion, transform.position, Quaternion.identity);
        }

        else

            if (Random.value >= 0.1f && Random.value < 0.3f)
        {
            Instantiate(droppedCoin, transform.position, Quaternion.identity);
        }
    }
}
