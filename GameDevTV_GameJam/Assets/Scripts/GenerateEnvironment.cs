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
    [SerializeField] Object enemyPrefab;
    [SerializeField] private int amountOfEnemies;
    
    private static List<GameObject> _islands;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _islands = new List<GameObject>();
        player = FindObjectOfType<PlayerController>().gameObject;
        gameManager = FindObjectOfType<GameManager>();
        RandomizeMap();
        GenerateFloor();
        MovePlayer();
        GenerateBorder();
        GenerateObjects();
        SpawnEnemy(amountOfEnemies);
    }

    public void SpawnEnemy(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            int xCoord = 0; 
            int yCoord = 0;
            
            //Randomly set X and Y
            if (Random.Range(0,100) > 50)
            {
                xCoord = (int) Random.Range(player.transform.position.x + 3, floorX);
            }
            else
            {
                xCoord = (int) Random.Range(0, player.transform.position.x - 3);
            }
            if (Random.Range(0,100) > 50)
            {
                yCoord = (int) Random.Range(player.transform.position.y + 3, floorY); 
            }
            else
            {
                yCoord = (int) Random.Range(0, player.transform.position.y - 3);
            }
            
            Vector2 spawnLocation = new Vector2(xCoord, yCoord);
            Object newEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            gameManager.AddEnemyCount();
        }
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

    public void InstantiateIsland(int x, int y)
    {
        int counter = 0;
        bool valid = CanSpawn(x, y);
        while (!valid && counter < 10)
        {
            valid = CanSpawn(x + 1, y + 1);
            counter++;
        }

        if (valid)
        {
            GameObject newIsland = Instantiate(islandPrefab, new Vector2(x, y), Quaternion.identity);
            newIsland.transform.parent = this.transform;
            newIsland.transform.position = new Vector3(x, y, 0);
            BoxCollider2D col = newIsland.GetComponent<BoxCollider2D>();
            gameManager.AddIslandCount();
            _islands.Add(newIsland);
        }
    }

    private static bool CanSpawn(int x, int y)
    {
        float islandWidth = 4;
        float islandHeight = 4;
        if (_islands.Count > 0)
        {
            foreach (var island in _islands)
            {
                //X1 = Left
                //x2 = Right
                //Y1 = Top
                //Y2 = Bottom

                //Island to spawn
                float RectA_X1 = x - islandWidth;
                float RectA_X2 = x + islandWidth;
                float RectA_Y1 = y + islandHeight;
                float RectA_Y2 = y - islandHeight;
                //Existing Island
                float RectB_X1 = island.GetComponent<IslandGenerator>().islandCenterX - islandWidth;
                float RectB_X2 = island.GetComponent<IslandGenerator>().islandCenterX + islandWidth;
                float RectB_Y1 = island.GetComponent<IslandGenerator>().islandCenterY + islandHeight;
                float RectB_Y2 = island.GetComponent<IslandGenerator>().islandCenterY - islandHeight;
                if (RectA_X1 < RectB_X2 && RectA_X2 > RectB_X1 &&
                    RectA_X1 > RectB_X2 && RectA_X2 < RectB_X1 &&
                    RectA_Y1 > RectB_Y2 && RectA_Y2 < RectB_Y1 && 
                    RectA_Y1 < RectB_Y2 && RectA_Y2 > RectB_Y1)
                {
                    Debug.Log("Islands Overlap");
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
        newFloorTile.layer = 2;
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
        FindObjectOfType<Camera>().ViewportToWorldPoint(player.transform.position);
    }

    void CanGenerateIsland(int x, int y)
    {
        RaycastHit hit;
        var temp = Physics.Raycast(new Vector3(x, y, 0), Vector3.down, out hit, 30);
        //Debug.Log("CanGenerateIsland: "+temp);
        if (temp)
        { 
            Quaternion spawnRotation = Quaternion.identity;
            Vector3 boxOverlap = new Vector3(
                islandPrefab.gameObject.GetComponent<IslandGenerator>().islandHeight, 
                islandPrefab.gameObject.GetComponent<IslandGenerator>().islandWidth,
                0
                );
            Collider[] colliders = new Collider[1];
            int numCols = Physics.OverlapBoxNonAlloc(
                hit.point,
                boxOverlap,
                colliders,
                spawnRotation,
                0
            );
            
            Debug.Log("Num cols found: "+ numCols);
        }
    }
}
