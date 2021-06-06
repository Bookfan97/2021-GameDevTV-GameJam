using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text CoinsNum;
    [SerializeField] private TMP_Text ScoreNum;
    [SerializeField] private TMP_Text SpeedNum;
    [SerializeField] private GameObject[] Lives = new GameObject[3];
    [SerializeField] private Sprite aliveSprite;
    [SerializeField] private Sprite deadSprite;
    public int localCoinCount;
    public int totalCoinCount;
    public int islandCounter;
    public int enemyCount;
    public int maxEnemyCount = 3;
    public int totalEnemyKilled;
    public int lives = 3;
    private GenerateEnvironment env;
    private PlayerController player;
    private float initalMoveSpeed;
    public bool gameOver = false;
    public bool isPaused = false;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    private void Awake()
    {
        int gameHandlerCount = FindObjectsOfType<GameManager>().Length;
        if (gameHandlerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        env = FindObjectOfType<GenerateEnvironment>();
        ResetCounters();
        initalMoveSpeed = player.moveSpeed;
        cursorTexture.width *= 10;
        cursorTexture.height *= 10; 
        //UI
        CoinsNum = GameObject.Find("CoinAmountText").GetComponent<TMP_Text>();
        ScoreNum = GameObject.Find("ScoreNum").GetComponent<TMP_Text>();
        SpeedNum = GameObject.Find("SpeedNum").GetComponent<TMP_Text>();
        Lives[2] = GameObject.Find("3Lives");
        Lives[1] = GameObject.Find("2Lives");
        Lives[0] = GameObject.Find("1Lives");
    }

    private void Update()
    {
        if (gameOver == false && isPaused == false)
        {
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                
                CoinsNum = GameObject.Find("CoinAmountText").GetComponent<TMP_Text>();
                ScoreNum = GameObject.Find("ScoreNum").GetComponent<TMP_Text>();
                SpeedNum = GameObject.Find("SpeedNum").GetComponent<TMP_Text>();
                Lives[2] = GameObject.Find("3Lives");
                Lives[1] = GameObject.Find("2Lives");
                Lives[0] = GameObject.Find("1Lives");

                ScoreNum.text = PlayerScore().ToString();
                CoinsNum.text = localCoinCount.ToString();
                SpeedNum.text = player.moveSpeed.ToString();
                UpdateLivesUI();

                if (enemyCount < maxEnemyCount)
                {
                    env.SpawnEnemy(maxEnemyCount - enemyCount);
                }
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < lives; i++)
        {
            Lives[i].SetActive(true);
            Lives[i].GetComponent<Image>().sprite = aliveSprite;
        }

        for (int i = lives; i < Lives.Length; i++)
        {
            Lives[i].GetComponent<Image>().sprite = deadSprite;
        }
    }

    private void ResetCounters()
    {
        localCoinCount = 0;
        totalCoinCount = 0;
        totalEnemyKilled = 0;
        maxEnemyCount = 3;
        lives = 3;
        gameOver = false;
        //Destroy(env);
        //env = new GenerateEnvironment();
    }
 
    public void DepositLocalCoin()
    {
        totalCoinCount += localCoinCount;
        localCoinCount = 0;
        player.moveSpeed = initalMoveSpeed;
    }

    public void AddCoinCount()
    {
        localCoinCount++;
        if (localCoinCount % 5 == 1)
        {
            player.moveSpeed = (player.moveSpeed - GetLocalCoinCount() % 2);
            if (player.moveSpeed < 1.0f)
            {
                player.moveSpeed = 0.5f;
            }
        }
        if ((localCoinCount + totalCoinCount) % 5 == 1)
        {
            maxEnemyCount++;
        }
    }

    public int GetLocalCoinCount()
    {
        return localCoinCount;
    }
    
    public void AddIslandCount()
    {
        islandCounter++;
    }
    
    public void RemoveIslandCount()
    {
        islandCounter--;
        env.InstantiateIsland(Random.Range(1, env.floorX-1), Random.Range(1, env.floorY-1));
    }
    
    public void AddEnemyCount()
    {
        enemyCount++;
    }
    
    public void RemoveEnemyCount()
    {
        enemyCount--;
        totalEnemyKilled++;
    }
    
    public void RemoveLivesCount()
    {
        lives--;
    }

    public void resetGame()
    {
        ResetCounters();
    }

    public int PlayerScore()
    {
        return totalCoinCount * 5 + totalEnemyKilled;
    }
}
