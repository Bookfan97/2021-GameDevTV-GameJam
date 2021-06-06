using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCollider : MonoBehaviour
{
    [SerializeField] private GameObject mist;
    private GameManager _gameManager;
    private bool hasCollide = false;
    private GenerateEnvironment env;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        env = FindObjectOfType<GenerateEnvironment>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                _gameManager.AddCoinCount();
                _gameManager.RemoveIslandCount();
                Instantiate(mist, this.gameObject.transform.parent.position, Quaternion.identity);
                Destroy(this.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().NewRandomPoint();
        }
        if (collision.gameObject.CompareTag("Island") || collision.gameObject.GetComponentsInChildren<IslandCollider>().Length > 0)
        {
            Debug.Log("Island overlap");
            Destroy(collision.gameObject);
        }
    }
}