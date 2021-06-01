using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        ResetCounters();
    }

    private void ResetCounters()
    {
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCointCount()
    {
        coinCount++;
        Debug.Log("Coins: "+ coinCount);
    }
}
