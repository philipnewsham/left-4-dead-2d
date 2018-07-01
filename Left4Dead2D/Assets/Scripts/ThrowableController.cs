using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    public GameObject molotov;
    public float force;
    public Transform parent;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject grenadeClone = Instantiate(molotov, transform);
            grenadeClone.transform.SetParent(parent);
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            grenadeClone.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90.0f);

            grenadeClone.GetComponent<Rigidbody2D>().AddRelativeForce(grenadeClone.transform.up * force);
        }
	}
}
