using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int localCoinCount;
    public int totalCoinCount;
    public int islandCounter;
    public int enemyCount;
    public int maxEnemyCount = 3;
    public int lives = 6;
    private GenerateEnvironment env;
    private PlayerController player;
    private float initalMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        env = FindObjectOfType<GenerateEnvironment>();
        ResetCounters();
        initalMoveSpeed = player.moveSpeed;
    }

    private void Update()
    {
        if (enemyCount < maxEnemyCount)
        {
            env.SpawnEnemy(maxEnemyCount - enemyCount);
        }
    }

    private void ResetCounters()
    {
        localCoinCount = 0;
        maxEnemyCount = 3;
        lives = 6;
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
    }
    
    public void RemoveLivesCount()
    {
        lives--;
    }

    public void resetGame()
    {
        throw new NotImplementedException();
    }

    public int PlayerScore()
    {
        throw new NotImplementedException();
    }
}
