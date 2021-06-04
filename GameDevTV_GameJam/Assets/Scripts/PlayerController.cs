using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] Transform firePoint;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float projectileForce = 20f;
    [SerializeField] private GameObject cannon = null;
    [SerializeField] private GameObject gameOver;
    [SerializeField] float invincibleLength=3;
    private float invincibleCounter;
    private Rigidbody2D playerRb, cnRB;
    private SpriteRenderer sr;
    private GameManager manager;
    private Vector2 movement, mousePos;
    private Camera camera;
    private float cannonOffsetX, cannonOffsetY;
    public bool canFire;
    
    void Start()
    {
        //Initialize Components
        gameOver = GameObject.Find("GameOver");
        manager = FindObjectOfType<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();
        sr = GetComponent <SpriteRenderer>();
        camera = FindObjectOfType<Camera>();
        canFire = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
        {
            Damage();

            if (manager.lives <= 0)
            {
                canFire = false;
                manager.gameOver = true;
                gameOver.transform.GetChild(0).gameObject.SetActive(true);
                gameOver.GetComponent<GameOver>().GameIsOver();
                Time.timeScale = 0.0f;
            }
        }
    }

    private void Damage()
    {
        if (invincibleCounter <= 0)
        {
            canFire = false;
            invincibleCounter = invincibleLength; 
            this.gameObject.tag = "Untagged";
            sr.color= new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            manager.RemoveLivesCount();
        }
    }

    void Update()
    {
        //Get Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
     
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        
        Vector2 direction = new Vector2(transform.position.x - mousePosition.x, transform.position.y - mousePosition.y);
        if (Input.GetButtonDown("Fire1") && canFire)
        { 
            Fire(-direction);
        }
        
        if(invincibleCounter>0)
        {
            invincibleCounter -= Time.deltaTime;
            if(invincibleCounter<=0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                this.gameObject.tag = "Player";
                canFire = true;
            }
        }
    }

    void FixedUpdate()
    {
        FixedPlayerMovement();
    } 

    //Player movement function, updates cannon position to player
    private void FixedPlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.deltaTime);
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        if (movement.x != 0 || movement.y != 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
           // cnRB.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
    
    //Fires Projectiles
    private void Fire(Vector2 direction)
    {
        
        GameObject projectile = Instantiate(cannonBall, firePoint.position, firePoint.rotation);
        Rigidbody2D rbProj = projectile.GetComponent<Rigidbody2D>();
        //projectile.transform.position = firePoint.position;
        rbProj.AddForce(direction * projectileForce, ForceMode2D.Impulse);
    }
}
