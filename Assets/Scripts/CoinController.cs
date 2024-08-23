using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinController : MonoBehaviour
{
    private Collider rb;

    void Start()
    {
        rb = GetComponent<Collider>();
    }

    private void Update()
    {
        rb.isTrigger = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Equals("Sphere(Clone)"))
        {
            GameObject.Destroy(rb.gameObject);

        }
        if (collision.gameObject.name.Equals("Green(Clone)"))
        {
            rb.isTrigger = true;

        }
        if (collision.gameObject.name.Equals("Red(Clone)"))
        {
            rb.isTrigger = true;
        }
    }
}
