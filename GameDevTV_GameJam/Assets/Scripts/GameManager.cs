using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int coinCount;
    private int islandCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetCounters();
    }

    private void ResetCounters()
    {
        coinCount = 0;
    }
    

    public void AddCointCount()
    {
        coinCount++;
    }
    
    public void AddIslandCount()
    {
        islandCounter++;
        Debug.Log(islandCounter);
    }
    
    public void RemoveIslandCount()
    {
        islandCounter--;
        Debug.Log(islandCounter);
    }
}
