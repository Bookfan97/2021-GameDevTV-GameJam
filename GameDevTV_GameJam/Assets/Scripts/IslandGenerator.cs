using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    [SerializeField] private int islandWidth = 2;
    [SerializeField] private int islandHeight = 2;
    
    [SerializeField] private GameObject islandSpritePrefab;
    [SerializeField] private Sprite[] floorSprite;
    [SerializeField] private Sprite[] bottomBorderSprite;
    [SerializeField] private Sprite[] topBorderSprite;
    [SerializeField] private Sprite[] leftBorderSprite;
    [SerializeField] private Sprite[] rightBorderSprite;
    [SerializeField] private Sprite[] decorationBorderSprite;
    [SerializeField] private Sprite URCornerSprite;
    [SerializeField] private Sprite BRCornerSprite;
    [SerializeField] private Sprite ULCornerSprite;
    [SerializeField] private Sprite BLCornerSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        var boxCollider2D = this.GetComponent<BoxCollider2D>();
        boxCollider2D.size = new Vector2(islandWidth+2, islandHeight+2);
        boxCollider2D.offset = new Vector2(0.5f,0.5f);
        GenerateBorder();
        GenerateFloor();
    }
    
    private void GenerateBorder()
    {
        for (int x = 0; x < islandWidth; x++)
        {
            //Bottom Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(x, -1, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], 0);
            }
            else
            {
                InstantiateSpriteTile(x, -1, bottomBorderSprite[Random.Range(0, bottomBorderSprite.Length)]);
            }
            
            //Top Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(x, islandHeight, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], 180);
            }
            else
            {
                InstantiateSpriteTile(x, islandHeight, topBorderSprite[Random.Range(0, topBorderSprite.Length)]);
            }
        }
        for (int y = 0; y < islandHeight; y++)
        {
            //Left Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(-1, y, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], -90);
            }
            else
            {
                InstantiateSpriteTile(-1, y, leftBorderSprite[Random.Range(0, leftBorderSprite.Length)]);    
            }
            
            //Right Wall
            if (Random.Range(0,100) < 20)
            {
                InstantiateBorderDecorationTile(islandWidth, y, decorationBorderSprite[Random.Range(0, decorationBorderSprite.Length)], 90);  
            }
            else
            {
                InstantiateSpriteTile(islandWidth, y, rightBorderSprite[Random.Range(0, rightBorderSprite.Length)]); 
            }
            
        }
        InstantiateSpriteTile(-1, -1, BLCornerSprite); //Bottom Left
        InstantiateSpriteTile(-1, islandHeight, ULCornerSprite); //Top left
        InstantiateSpriteTile(islandWidth, -1, BRCornerSprite); //Bottom Right
        InstantiateSpriteTile(islandWidth, islandHeight, URCornerSprite); //Top Right
    }

    private void GenerateFloor()
    {
        for (int x = 0; x < islandWidth; x++)
        {
            for (int y = 0; y < islandWidth; y++)
            {
                InstantiateSpriteTile(x,y, floorSprite[Random.Range(0, floorSprite.Length)]);
            }
        }
    }
    
    private void InstantiateSpriteTile(int x, int y, Sprite islandSprite)
    {
            GameObject newSpriteTile = Instantiate(islandSpritePrefab, new Vector2(x, y), Quaternion.identity);
            newSpriteTile.GetComponent<SpriteRenderer>().sprite = islandSprite;
            newSpriteTile.GetComponent<SpriteRenderer>().sortingOrder = 5;
            newSpriteTile.transform.SetParent(this.transform, false);
    }
    private void InstantiateBorderDecorationTile(int x, int y, Sprite islandSprite, int rotation)
    {
            GameObject newSpriteTile = Instantiate(islandSpritePrefab, new Vector2(x, y), Quaternion.identity);
            newSpriteTile.GetComponent<SpriteRenderer>().sprite = islandSprite;
            newSpriteTile.GetComponent<SpriteRenderer>().sortingOrder = 5;
            newSpriteTile.gameObject.transform.Rotate(0,0,rotation);
            newSpriteTile.transform.SetParent(this.transform, false);
    }
}
