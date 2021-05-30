using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float projectileForce = 20f;
    [SerializeField] Transform firePoint;
    private PlayerController player;
    public bool aggro = false, canFire = false;
    public Vector3 targetPosition;
    public float attackCounter, moveCounter;
    public float waitAfterAttack = 3, waitToMove = 3, minDistance = 2, maxDistance = 5;
    public int batAttackSpeed = 5;

    public float patrolTest = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        targetPosition = Vector3.zero;
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
        //CheckForNewPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, batAttackSpeed * Time.deltaTime);
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
                patrolTest++;
                targetPosition = new Vector3(Random.Range(-10,10),Random.Range(-10,10),0);
            }
        }
    }
    
    private void Fire()
    {
        Debug.Log("FIRE");
        GameObject projectile = Instantiate(cannonBall, firePoint.position, firePoint.rotation);
        Rigidbody2D rbProj = projectile.GetComponent<Rigidbody2D>();
        rbProj.AddForce(player.gameObject.transform.position * projectileForce, ForceMode2D.Impulse);
        attackCounter = waitAfterAttack;
    }
}
