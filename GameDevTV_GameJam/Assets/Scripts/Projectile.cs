using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool enemy;
    private Rigidbody2D rb;
    private float speed = 10;
    private void Awake()
    {
        enemy = this.gameObject.tag == "EnemyBullet";
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!enemy)
        {
            rb.velocity = transform.right * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this);
    }
}
