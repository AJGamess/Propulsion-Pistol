using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float recoilForce = 5f;
    [SerializeField] int maxAmmoCount = 3;
    [SerializeField] int ammoCount = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //reset ammo count when player is grounded
        if (PlayerController.isGrounded)
        {
            ammoCount = maxAmmoCount;
        }
    }
}
