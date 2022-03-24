using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float BulletSpeed;
    Rigidbody rb;
    public float DestroyTime;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * BulletSpeed;
        Invoke("DestroyItself", DestroyTime);
    }
    void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}
