using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] Sprite[] fireSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0,100) < 25)
        {
            this.GetComponent<SpriteRenderer>().sprite = fireSprites[Random.Range(0, fireSprites.Length)];
        }
    }
}
