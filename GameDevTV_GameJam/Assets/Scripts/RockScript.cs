using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [SerializeField] private Sprite[] rockSprites;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite = rockSprites[Random.Range(0, rockSprites.Length)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Island") || collision.gameObject.GetComponentsInChildren<IslandCollider>().Length > 0)
        {
            Destroy(collision.gameObject);
        }
    }
}
