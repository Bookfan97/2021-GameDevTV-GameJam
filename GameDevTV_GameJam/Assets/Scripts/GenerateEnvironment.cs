using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    [SerializeField] private static int buffer = 10;
    [SerializeField] private GameObject player;
    [SerializeField] public int floorX = 20;
    [SerializeField] public int floorY = 20;
    [SerializeField] private GameObject waterTile;
    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private Sprite[] borderSprites;
    [SerializeField] private GameObject islandPrefab;
    
    private static List<GameObject> _islands;
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
                if (Random.Range(0, 1000) < 5)
                {
                    InstantiateIsland(x,y);
                }
            }
        }
    }

    private void InstantiateIsland(int x, int y)
    {
        bool valid = CanSpawn(x, y);
        if (valid)
        {
            GameObject newIsland = Instantiate(islandPrefab, new Vector2(x, y), Quaternion.identity);
            newIsland.transform.parent = this.transform;
            newIsland.transform.position = new Vector3(x, y, 0);
            BoxCollider2D col = newIsland.GetComponent<BoxCollider2D>();
            //Physics2D.OverlapCollider(newIsland.GetComponent<BoxCollider>(), filter, results);
            //Destroy(newIsland);
            _islands.Add(newIsland);
        }
    }

    private static bool CanSpawn(int x, int y)
    {
        
        if (_islands.Count <= 0)
        {
            return true;
        }
        else
        {
            foreach (var island in _islands)
            {
                IslandGenerator IG = island.GetComponent<IslandGenerator>();
                if ((IG.islandCenterX - IG.islandWidth) + buffer < (x - IG.islandWidth) &&
                    (x + IG.islandWidth) < (IG.islandCenterX + IG.islandWidth) + buffer)
                {
                    return false;
                }
                else if ((IG.islandCenterY - IG.islandHeight) + buffer < (y + IG.islandHeight) &&
                         (y - IG.islandHeight) < (IG.islandCenterY + IG.islandHeight) + buffer)
                {
                    return false;
                }
            }
        }
        return true;
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
