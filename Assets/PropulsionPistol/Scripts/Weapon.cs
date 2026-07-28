using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float recoilForce = 5f;
    public int maxAmmoCount = 3;
    public int ammoCount = 3;


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
