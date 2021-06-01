using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] mistGameObjects;
    [SerializeField] private Sprite[] cloudSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var mist in mistGameObjects)
        {
            mist.gameObject.GetComponent<SpriteRenderer>().sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
            mist.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 20;
        }
    }
}
