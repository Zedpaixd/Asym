using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale *= -1;
    }

}
