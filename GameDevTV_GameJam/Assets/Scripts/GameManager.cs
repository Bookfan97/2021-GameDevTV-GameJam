using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int coinCount;
    private int islandCounter;
    private int enemyCount;
    private int maxEnemyCount = 3;

    private GenerateEnvironment env;
    // Start is called before the first frame update
    void Start()
    {
        env = FindObjectOfType<GenerateEnvironment>();
        ResetCounters();
    }

    private void Update()
    {
        if (coinCount % 5 == 1)
        {
            maxEnemyCount++;
        }

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
