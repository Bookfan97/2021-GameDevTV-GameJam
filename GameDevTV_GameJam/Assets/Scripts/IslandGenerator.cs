using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private bool isFort = false;
    public float islandCenterX, islandCenterY;
    [SerializeField] public int islandWidth = 2;
    [SerializeField] public int islandHeight = 2;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject islandSpritePrefab;
    [SerializeField] private GameObject fortSpritePrefab;
    [SerializeField] private Sprite[] floorSprite;
    [SerializeField] private Sprite[] bottomBorderSprite;
    [SerializeField] private Sprite[] topBorderSprite;
    [SerializeField] private Sprite[] leftBorderSprite;
    [SerializeField] private Sprite[] rightBorderSprite;
    [SerializeField] private Sprite[] decorationBorderSprite;
    [SerializeField] private Sprite[] fortWallSprite;
    [SerializeField] private Sprite URCornerSprite;
    [SerializeField] private Sprite BRCornerSprite;
    [SerializeField] private Sprite ULCornerSprite;
    [SerializeField] private Sprite BLCornerSprite;
    [SerializeField] private Sprite URFortCornerSprite;
    [SerializeField] private Sprite BRFortCornerSprite;
    [SerializeField] private Sprite ULFortCornerSprite;
    [SerializeField] private Sprite BLFortCornerSprite;

    private GenerateEnvironment _environment;
    private GameObject toUse;
    // Start is called before the first frame update
    void Start()
    {
        if (isFort)
        {
            toUse = fortSpritePrefab;
            islandHeight += 2;
            islandWidth += 2;
        }
        else
        {
            toUse = islandSpritePrefab;
        }
        _environment = FindObjectOfType<GenerateEnvironment>();
        var boxCollider2D = this.GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(islandWidth+2, islandHeight+2);
        boxCollider2D.offset = new Vector2(0.5f,0.5f);
        GenerateBorder();
        GenerateFloor();
        islandCenterX = (this.transform.position.x + islandWidth) - 1.5f;
        islandCenterY = (this.transform.position.y + islandHeight) - 1.5f;
        if (isFort)
        {
            GenerateFortWalls();
        }
        else
        {
            GenerateCoin();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collider: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            _environment.InstantiateIsland(Random.Range(1, _environment.floorX-1), Random.Range(1, _environment.floorY-1));
            DestroyImmediate(this.gameObject);
        }
    }
    
    private void GenerateBorder()
    {
        for (int x = 0; x < islandWidth; x++)
        {
            //Bottom Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(x, -1, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], 0, 5);
            }
            else
            {
                InstantiateSpriteTile(x, -1, bottomBorderSprite[Random.Range(0, bottomBorderSprite.Length)], 5);
            }
            
            //Top Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(x, islandHeight, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], 180, 5);
            }
            else
            {
                InstantiateSpriteTile(x, islandHeight, topBorderSprite[Random.Range(0, topBorderSprite.Length)], 5);
            }
        }
        for (int y = 0; y < islandHeight; y++)
        {
            //Left Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(-1, y, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], -90, 5);
            }
            else
            {
                InstantiateSpriteTile(-1, y, leftBorderSprite[Random.Range(0, leftBorderSprite.Length)], 5);    
            }
            
            //Right Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(islandWidth, y, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], 90, 5);  
            }
            else
            {
                InstantiateSpriteTile(islandWidth, y, rightBorderSprite[Random.Range(0, rightBorderSprite.Length)], 5); 
            }
            
        }
        InstantiateSpriteTile(-1, -1, BLCornerSprite, 5); //Bottom Left
        InstantiateSpriteTile(-1, islandHeight, ULCornerSprite, 5); //Top left
        InstantiateSpriteTile(islandWidth, -1, BRCornerSprite, 5); //Bottom Right
        InstantiateSpriteTile(islandWidth, islandHeight, URCornerSprite, 5); //Top Right
    }

    private void GenerateFloor()
    {
        for (int x = 0; x < islandWidth; x++)
        {
            for (int y = 0; y < islandWidth; y++)
            {
                InstantiateSpriteTile(x,y, floorSprite[Random.Range(0, floorSprite.Length)], 5);
            }
        }
    }
    
    private void GenerateFortWalls()
    {
        for (int x = 1; x < islandWidth-1; x++)
        {
            //Bottom Wall
            InstantiateBorderDecorationTile(x, 0, fortWallSprite[Random.Range(0, fortWallSprite.Length)], 90, 8); 
                
            //Top Wall
            InstantiateBorderDecorationTile(x, islandHeight-1, fortWallSprite[Random.Range(0, fortWallSprite.Length)], 90, 8); 
        }
        for (int y = 1; y < islandHeight-1; y++)
        {
            //Left Wall
            InstantiateBorderDecorationTile(0, y, fortWallSprite[Random.Range(0, fortWallSprite.Length)], 0, 8);
            
            //Right Wall
            InstantiateBorderDecorationTile(islandWidth-1, y, fortWallSprite[Random.Range(0, fortWallSprite.Length)], 0, 8);
        }
        
        InstantiateSpriteTile(0, 0, BLFortCornerSprite, 8); //Bottom Left
        InstantiateSpriteTile(0, islandHeight-1, ULFortCornerSprite, 8); //Top left
        InstantiateSpriteTile(islandWidth-1, 0, BRFortCornerSprite, 8); //Bottom Right
        InstantiateSpriteTile(islandWidth-1, islandHeight-1, URFortCornerSprite, 8); //Top Right
    }
    
    private void InstantiateSpriteTile(int x, int y, Sprite islandSprite, int sortOrder)
    {
            GameObject newSpriteTile = Instantiate(toUse, new Vector2(x, y), Quaternion.identity);
            newSpriteTile.GetComponent<SpriteRenderer>().sprite = islandSprite;
            newSpriteTile.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
            newSpriteTile.transform.SetParent(this.transform, false);
    }
    
    private void InstantiateBorderDecorationTile(int x, int y, Sprite islandSprite, int rotation, int sortOrder)
    {
            GameObject newSpriteTile = Instantiate(toUse, new Vector2(x, y), Quaternion.identity);
            newSpriteTile.GetComponent<SpriteRenderer>().sprite = islandSprite;
            newSpriteTile.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
            newSpriteTile.gameObject.transform.Rotate(0,0,rotation);
            newSpriteTile.transform.SetParent(this.transform, false);
    }

    private void GenerateCoin()
    {
        GameObject newCoin = Instantiate(coinPrefab, new Vector2(.5f, .5f), Quaternion.identity);
        //newCoin.GetComponent<SpriteRenderer>().sprite = islandSprite;
        //newCoin.gameObject.transform.position = new Vector3(islandCenterX, islandCenterY, 0);
        newCoin.GetComponent<SpriteRenderer>().sortingOrder = 10;
        newCoin.transform.SetParent(this.transform, false);
    }
}
