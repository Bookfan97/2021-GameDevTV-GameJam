using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLISION" +collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.CompareTag("Island"))
        {
            Debug.Log("Island overlap");
            Destroy(this);
        }
    }
}
