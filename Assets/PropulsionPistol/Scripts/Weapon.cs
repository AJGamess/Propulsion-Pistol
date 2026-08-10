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
    public float nextShotTime = 0f;

    // Static event that the AmmoUI can subscribe to for ammo count changes
    public static event System.Action<string, int, int> OnAmmoCountChanged;


    public void Equip()
    {
        // When you swap a weapon, update the UI to reflect the new weapon's ammo count
        UpdateUI();
    }

    // Method to handle shooting the weapon
    public bool TryShootWeapon()
    {
        // Check if the current time is greater than or equal to the next allowed shot time
        if (Time.time < nextShotTime)
        {
            Debug.Log("Weapon is on cooldown. Next shot available in: " + (nextShotTime - Time.time) + " seconds.");
            return false;
        }
        // Don't fire if there's no ammo left
        else if (ammoCount <= 0)
        {
            Debug.Log("No ammo left! Reload the weapon.");
            return false;
        }
        // Decrease the ammo count and invoke the ammo count changed event
        else
        {
            nextShotTime = Time.time + shotCooldown;
            ammoCount--;
            UpdateUI();
            return true;
        }
    }

    // Reload the weapon to its maximum ammo count
    public void ReloadWeapon()
    {
        // Trigger only if the ammo count is less than the maximum ammo count
        if (ammoCount < maxAmmoCount)
        {
            ammoCount = maxAmmoCount;
            UpdateUI();
        }
    }

    // Update the ammo count and notify any subscribers about the change
    public void UpdateUI()
    {
        OnAmmoCountChanged?.Invoke(weaponName, ammoCount, maxAmmoCount);
    }
}
