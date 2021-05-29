using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController player;
    private bool aggro = false;
    private Vector3 targetPosition;
    private float attackCounter;
    public float waitAfterAttack = 3, minDistance;
    public int batAttackSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();//GetComponent<PlayerController>().gameObject;
        aggro = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        if (aggro)
        {
            AttackPlayer();
        }
        else
        {
            CheckDistanceToPlayer();
        }
    }

    private void CheckDistanceToPlayer()
    {
        float distance = 0;
        if (distance <= minDistance)
        {
            aggro = true;
        }
    }

    private void AttackPlayer()
    {
        if (attackCounter <= 0)
        {
            if (targetPosition == Vector3.zero)
            {
                targetPosition = player.gameObject.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, batAttackSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) <= .1f)
            {
                attackCounter = waitAfterAttack;
                targetPosition = Vector3.zero;
            }
        }
    }
}
