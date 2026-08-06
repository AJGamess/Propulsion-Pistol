using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    private TMP_Text ammoText;
    private TMP_Text weaponNameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the ammo count and weapon name in the UI
        ammoText.text = "Ammo: " + FindAnyObjectByType<Weapon>().ammoCount + " / " + FindAnyObjectByType<Weapon>().maxAmmoCount;
        weaponNameText.text = "Weapon: " + FindAnyObjectByType<Weapon>().weaponName;
    }
}
