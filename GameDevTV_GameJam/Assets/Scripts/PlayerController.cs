using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] Transform firePoint;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float projectileForce = 20f;
    [SerializeField] private GameObject cannon = null;
    private Rigidbody2D playerRb, cnRB;
    private SpriteRenderer sr;
    private Vector2 movement, mousePos;
    private Camera camera;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        cnRB = cannon.GetComponent<Rigidbody2D>();
        sr = GetComponent <SpriteRenderer>();
        camera = FindObjectOfType<Camera>();//GetComponent<Camera>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        FixedPlayerMovement();
       // FixedCannonMovement();
    }

    private void FixedCannonMovement()
    {
       
        Vector2 lookDir = mousePos - cnRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        cnRB.rotation = angle;
    }

    private void FixedPlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.deltaTime);
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        if (movement.x != 0 || movement.y != 0)
        {
            //cnRB.position = new Vector2(playerRb.position.x + cannon.gameObject.transform.position.x,playerRb.position.y + cannon.gameObject.transform.position.y);
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
    
    private void Fire()
    {
        GameObject projectile = Instantiate(cannonBall, firePoint.position, firePoint.rotation);
        Rigidbody2D rbProj = projectile.GetComponent<Rigidbody2D>();
        rbProj.AddForce(mousePos * projectileForce, ForceMode2D.Impulse);
    }
}
