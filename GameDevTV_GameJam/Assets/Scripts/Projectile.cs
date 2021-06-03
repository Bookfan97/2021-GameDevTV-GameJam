using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Awake()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this);
    }
}
