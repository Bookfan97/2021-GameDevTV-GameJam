using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int coinCount;
    public int islandCounter;
    public int enemyCount;
    public int maxEnemyCount = 3;
    public int lives = 6;
    private GenerateEnvironment env;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        env = FindObjectOfType<GenerateEnvironment>();
        ResetCounters();
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
        coinCount = 0;
        maxEnemyCount = 3;
    }

    public void AddCoinCount()
    {
        coinCount++;
        if (coinCount % 5 == 1)
        {
            maxEnemyCount++;
            player.moveSpeed = (player.moveSpeed - GetCoinCount() % 2);
        }

    }

    public int GetCoinCount()
    {
        return coinCount;
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
}
