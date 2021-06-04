using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject explode;
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float projectileForce = 20f;
    [SerializeField] Transform firePoint;
    private PlayerController player;
    public bool aggro = false, canFire = false;
    public Vector3 targetPosition;
    public float attackCounter, moveCounter;
    public float waitAfterAttack = 3, waitToMove = 3, minDistance = 2, maxDistance = 8;
    public int attackSpeed = 5;
    public float patrolTest = 0;
    private GenerateEnvironment env;
    private GameManager manager;
    public bool needNewPosition = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        targetPosition = Vector3.zero;
        env = FindObjectOfType<GenerateEnvironment>();
        firePoint.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToPlayer();
        
        if (moveCounter > 0)
        {
            moveCounter -= Time.deltaTime;
        }
        else
        {
            MoveToPosition();
        }

        if (aggro)
        {
            if (attackCounter > 0)
            {
                attackCounter -= Time.deltaTime;
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            CheckForNewPosition();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(explode, this.transform.position, Quaternion.identity); 
            manager.RemoveEnemyCount();
            env.SpawnEnemy(1);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void CheckDistanceToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            aggro = true;
        }
        else
        {
            aggro = false;
            attackCounter = waitAfterAttack;
        }
    } 

    private void AttackPlayer()
    {
        if (attackCounter <= 0)
        {
            Fire();
        }
    }

    private void MoveToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, attackSpeed * Time.deltaTime);
        Vector3 dir = this.transform.position - targetPosition;
        int angle = (int) (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void CheckForNewPosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) <= .1f || targetPosition == Vector3.zero)
        {
            moveCounter = waitToMove;
            if (aggro)
            {
                targetPosition = player.gameObject.transform.position + new Vector3(minDistance,minDistance,0);
            }
            else if(!aggro)
            {
                targetPosition = new Vector3(Random.Range(1,env.floorX),Random.Range(1,env.floorY),0);
            }
        }
    }
    
    private void Fire()
    {
        Vector2 direction = new Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
        GameObject projectile = Instantiate(cannonBall, firePoint.position, firePoint.rotation);
        projectile.tag = "EnemyBullet";
        Rigidbody2D rbProj = projectile.GetComponent<Rigidbody2D>();
        rbProj.AddForce(-direction * projectileForce, ForceMode2D.Impulse);
        attackCounter = waitAfterAttack;
    }
}
