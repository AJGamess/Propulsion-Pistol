using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Properties")]
    public string weaponName = "";
    public float recoilForce = 5f;
    public int maxAmmoCount = 3;
    public int ammoCount = 3;
    public float shotCooldown = 0.5f;  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReloadWeapon()
    {
        ammoCount = maxAmmoCount;
    }
}
