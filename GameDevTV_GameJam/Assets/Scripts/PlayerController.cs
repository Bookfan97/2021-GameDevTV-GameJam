using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent <SpriteRenderer>();
    }

    void Update()
    {
        float tempX = Input.GetAxisRaw("Horizontal");
        float tempY = Input.GetAxisRaw("Vertical");
        
        /*Debug.Log(tempX +", "+ tempY);
        if (Mathf.Abs(tempX) > 0)
        {*/
            movement.x = tempX;
        //}

        /*if (Mathf.Abs(tempY) > 0)
        {*/
            movement.y = tempY;
        //}
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        if (movement.x != 0 || movement.y != 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
