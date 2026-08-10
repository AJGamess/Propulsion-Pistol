using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text weaponNameText;

    private void OnEnable()
    {
        // Listen for the event when the script activates
        Weapon.OnAmmoCountChanged += UpdateUI;
    }

    private void OnDisable()
    {
        // Stop listening if the object is disabled
        Weapon.OnAmmoCountChanged -= UpdateUI;
    }

    // This function automatically runs ONLY when a gun shoots, reloads, or equips!
    private void UpdateUI(string weaponName, int currentAmmo, int maxAmmo)
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
        }

        if (weaponNameText != null)
        {
            weaponNameText.text = "Weapon: " + weaponName;
        }
    }
}