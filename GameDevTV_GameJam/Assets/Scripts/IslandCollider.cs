using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCollider : MonoBehaviour
{
    [SerializeField] private GameObject mist;
    private GameManager _gameManager;
    private bool addedCoin;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        addedCoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION" +collision.gameObject.tag+", "+ this.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!addedCoin)
            {
                _gameManager.AddCointCount();
                addedCoin = true;
            }
            Instantiate(mist, this.gameObject.transform.parent.position, Quaternion.identity);
            Destroy(this.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("Island"))
        {
            Debug.Log("Island overlap");
            Destroy(this);
        }
    }
}
