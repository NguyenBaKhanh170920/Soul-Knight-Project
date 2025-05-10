using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    public Image CoolDownIcon;
    [HideInInspector]
    public float cooldown;
    bool isCooldown;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
       isCooldown = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            CoolDownIcon.fillAmount += 1 / cooldown * Time.deltaTime;
            if (CoolDownIcon.fillAmount >= 1)
            {
                CoolDownIcon.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
