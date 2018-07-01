using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public GameObject bullet;
    public float force;
    public Transform parent;
    private WeaponContainer collection;
    private List<Weapon> weapons = new List<Weapon>();
    private Weapon currentWeapon;

    private int currentMaxAmmo;
    private int currentClipAmount;
    public Text ammoText;

    void Awake()
    {
        collection = WeaponContainer.Load(Path.Combine(Application.dataPath, "weapons.xml"));
        weapons = collection.weapons;
        GiveWeapon(weapons[0]);  
    }

    void GiveWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        currentClipAmount = currentWeapon.clipSize;
        currentMaxAmmo = currentWeapon.maxAmmo - currentClipAmount;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentClipAmount > 0)
        {
            currentClipAmount--;
            GameObject bulletClone = Instantiate(bullet, transform);
            bulletClone.transform.SetParent(parent);
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bulletClone.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90.0f);
            
            bulletClone.GetComponent<Rigidbody2D>().AddRelativeForce(bulletClone.transform.up * force);
            UpdateAmmoUI();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Reload()
    {
        int remainder = Mathf.Clamp(currentWeapon.clipSize - currentClipAmount, 0, currentMaxAmmo);

        currentClipAmount = Mathf.Clamp(currentClipAmount + remainder, 0, currentWeapon.clipSize);
        currentMaxAmmo = Mathf.Clamp(currentMaxAmmo - remainder, 0, currentWeapon.maxAmmo);
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = string.Format("{0}/{1}", currentClipAmount, currentMaxAmmo);
    }
}
