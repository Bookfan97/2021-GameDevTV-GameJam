using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public int floorX = 20;
    [SerializeField] public int floorY = 20;
    [SerializeField] private GameObject waterTile;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        RandomizeMap();
        GenerateFloor();
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void RandomizeMap()
    {
        floorX = Random.Range(20, 40);
        floorY = Random.Range(20, 40);
    }

    private void GenerateFloor()
    {
        for (int x = 0; x < floorX; x++)
        {
            for (int y = 0; y < floorY; y++)
            {
                InstantiateWaterTile(x, y);
            }
        }
    }

    private void InstantiateWaterTile(int x, int y)
    {
        GameObject newFloorTile = Instantiate(waterTile, new Vector2(x, y), Quaternion.identity); 
        newFloorTile.GetComponent<SpriteRenderer>().sortingOrder = 0;
        newFloorTile.transform.parent = this.transform;
    }
    
    private void MovePlayer()
    {
        player.transform.position = new Vector3(floorX / 2, floorY / 2, 0);
    }
}
