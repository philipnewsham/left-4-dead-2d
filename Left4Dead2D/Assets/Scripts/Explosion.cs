using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private List<Rigidbody2D> objects = new List<Rigidbody2D>();
    private CircleCollider2D circleCollider;
    public float force;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Rigidbody2D obj in objects)
            {
                float distance = Vector2.Distance(obj.transform.position, transform.position);
                Debug.Log(distance);
                float explosiveForce = Mathf.Lerp(0.0f, force, 1 - (distance / (circleCollider.radius * transform.localScale.x)));
                Debug.Log(explosiveForce);
                obj.AddForce((obj.transform.position - transform.position) * explosiveForce);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            if (!objects.Contains(rb))
            {
                objects.Add(rb);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (objects.Contains(other.GetComponent<Rigidbody2D>()))
        {
            objects.Remove(other.GetComponent<Rigidbody2D>());
        }
    }

}
