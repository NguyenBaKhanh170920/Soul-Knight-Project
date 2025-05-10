using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapCommand : MonoBehaviour
{
    int totalWeapon =0;
    public int currentWeaponIndex;
    public GameObject[] weapon;
    public GameObject weaponHolder;
    public GameObject currentWeapon;

    private bool state=true;//true=sung, false=kiem
    public ShootingScript gunScript;
    public SwordScript swordScript;
    // Start is called before the first frame update
    void Start()
    {
        totalWeapon = weaponHolder.transform.childCount;
        weapon=new GameObject[totalWeapon];
        for(int i = 0; i < totalWeapon; i++)
        {
            weapon[i]=weaponHolder.transform.GetChild(i).gameObject;
            weapon[i].SetActive(false);
        }
        weapon[0].SetActive(true);
        currentWeapon = weapon[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            NextWeapon();
            state=!state;
            ResetCooldown();
        }
        if (state)
        {
            gunScript.enabled = true;
            swordScript.enabled = false;
        }
        else
        {
            gunScript.enabled = false;
            swordScript.enabled = true;
        }
    }
    void NextWeapon()
    {
        if (currentWeaponIndex <= totalWeapon-1)
        {
            weapon[currentWeaponIndex].SetActive(false);
            currentWeaponIndex++;
            if (currentWeaponIndex >= totalWeapon)
            {
                currentWeaponIndex = 0;
            }
            weapon[currentWeaponIndex].SetActive(true);
            currentWeapon = weapon[currentWeaponIndex];
        }
    }
    void ResetCooldown()
    {
        if (gunScript != null)
        {
            gunScript.ResetCooldown();
        }
        if (swordScript != null)
        {
            swordScript.ResetCooldown();
        }
    }
}
