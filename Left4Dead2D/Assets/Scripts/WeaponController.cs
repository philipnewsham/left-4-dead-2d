using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bullet;
    public float force;
    public Transform parent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet, transform);
            bulletClone.transform.SetParent(parent);
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bulletClone.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90.0f);
            
            bulletClone.GetComponent<Rigidbody2D>().AddRelativeForce(bulletClone.transform.up * force);
        }
    }
}
