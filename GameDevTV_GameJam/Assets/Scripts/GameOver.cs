// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private HighScoreTable highScoreTable;
    [SerializeField] private GameObject NewHighScoreTable;
    [SerializeField] private InputField displayName;
    [SerializeField] private Text playerScore;
    [SerializeField] private GameObject InGameUI;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        highScoreTable = FindObjectOfType<HighScoreTable>();
    }
    

    public void ActivateNewScoreWindow()
    {
        NewHighScoreTable.SetActive(true);
    }

    public void RetryButton()
    {
        gameManager.resetGame();
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        highScoreTable.SendLeaderboard(gameManager.PlayerScore());
        highScoreTable.gameObject.SetActive(false);
        InGameUI.SetActive(true);
    }
    
    public void SubmitButton()
    {
        highScoreTable = FindObjectOfType<HighScoreTable>();
        if (displayName.gameObject.activeInHierarchy)
        {
            string name = displayName.text;
            
            highScoreTable.SubmitDisplayName(name);
        }
        highScoreTable.SendLeaderboard(gameManager.PlayerScore());
        NewHighScoreTable.gameObject.SetActive(false);
        highScoreTable.GetLeaderboard();
    }

    public void ActivateNewPlayerField()
    {
        displayName.gameObject.SetActive(true);
    }

    public void GameIsOver()
    {
        NewHighScoreTable.SetActive(true);
        InGameUI.SetActive(false);
        playerScore.text = ("Score: " + gameManager.PlayerScore()).ToString();
        displayName.gameObject.SetActive(false);
        highScoreTable.GetLeaderboard();
    }
}
