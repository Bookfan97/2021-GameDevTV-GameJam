using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public int floorX = 20;
    [SerializeField] public int floorY = 20;
    [SerializeField] private GameObject waterTile;
    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private Sprite[] borderSprites;
    [SerializeField] private GameObject islandPrefab;
    
    private List<GameObject> _islands;
    // Start is called before the first frame update
    void Start()
    {
        _islands = new List<GameObject>();
        player = FindObjectOfType<PlayerController>().gameObject;
        RandomizeMap();
        GenerateFloor();
        MovePlayer();
        GenerateBorder();
        GenerateObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void RandomizeMap()
    {
        floorX = Random.Range(40, 80);
        floorY = Random.Range(40, 80);
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

    private void GenerateBorder()
    {
        for (int x = -5; x < floorX +5; x++)
        {
            //Bottom Wall
            for (int j = -7; j < 0; j++)
            {
                InstantiateWallTile(x, j);
            }
            //Top Wall
            for (int j = 0; j < 5; j++)
            {
                InstantiateWallTile(x, floorY + j);
            }
        }

        for (int y = -5; y < floorY + 5; y++)
        {
            //Left Wall
            for (int j = -7; j < 0; j++)
            {
                InstantiateWallTile(j, y);
            }

            //Right Wall
            for (int j = 0; j < 7; j++)
            {
                InstantiateWallTile(floorX + j, y); 
            }
        }
    }

    private void GenerateObjects()
    {
        int count = 0;
        for (int x = 1; x < floorX - 1; x++)
        {
            for (int y = 1; y < floorY - 1; y++)
            {
                bool validPosition = true;
                Collider[] colliders = Physics.OverlapSphere(new Vector3(x, y, 0), 10f);
                foreach(Collider col in colliders)
                {
                    if(col.tag == "Island")
                    {
                        validPosition = false;
                    }
                }
                if (Random.Range(0, 1000) < 5 && validPosition)
                {
                    InstantiateIsland(x,y);
                }
            }
        }
    }

    private void InstantiateIsland(int x, int y)
    {
        GameObject newIsland = Instantiate(islandPrefab, new Vector2(x, y), Quaternion.identity); 
        //newIsland.GetComponent<SpriteRenderer>().sortingOrder = 3;
        newIsland.transform.parent = this.transform;
        newIsland.transform.position = new Vector3(x, y, 0);
        _islands.Add(newIsland);
    }

    private void InstantiateWaterTile(int x, int y)
    {
        GameObject newFloorTile = Instantiate(waterTile, new Vector2(x, y), Quaternion.identity); 
        newFloorTile.GetComponent<SpriteRenderer>().sortingOrder = 0;
        newFloorTile.transform.parent = this.transform;
    }
    
    private void InstantiateWallTile(int x, int y)
    {
        GameObject newBorderTile = Instantiate(borderPrefab, new Vector2(x, y), Quaternion.identity);
        newBorderTile.GetComponent<SpriteRenderer>().sprite = borderSprites[Random.Range(0, borderSprites.Length)];
        newBorderTile.GetComponent<SpriteRenderer>().sortingOrder = 11;
        newBorderTile.transform.parent = this.transform;
    }
    
    private void MovePlayer()
    {
        player.transform.position = new Vector3(floorX / 2, floorY / 2, 0);
    }
}
