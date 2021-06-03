using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortCollider : MonoBehaviour
{
    private GameManager _gameManager;
    private GenerateEnvironment env;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        env = FindObjectOfType<GenerateEnvironment>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("COLLISION" +collision.gameObject.tag+", "+ this.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameManager.DepositLocalCoin();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().targetPosition = new Vector3(Random.Range(1,env.floorX),Random.Range(1,env.floorY),0);
        }
        
        if (collision.gameObject.CompareTag("Island") || collision.gameObject.GetComponentsInChildren<IslandCollider>().Length > 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
