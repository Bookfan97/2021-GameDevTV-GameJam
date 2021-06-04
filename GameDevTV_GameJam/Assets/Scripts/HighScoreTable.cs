using System;
using System.Collections;
using System.Collections.Generic;
//using Packages.Rider.Editor;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Random = UnityEngine.Random;

public class HighScoreTable : MonoBehaviour
{
    public GameObject NewHighScoreWindow;
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] Button SubmitButton;
    [SerializeField] InputField HSName;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private Text displayNameText;
    private List<Transform> highscoreEntryTransformList;
    //private HighscoreEntry lowestEntry;

    private void Awake()
    {
        Login();
       // GetLeaderboard();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnError(PlayFabError error) 
    {
        Debug.Log("Login Failed");
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Login successful");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (name == null)
        {
            FindObjectOfType<GameOver>().ActivateNewPlayerField();
        }
        else
        {
            displayNameText.text = ("Display Name: " + name).ToString();
            SendLeaderboard(FindObjectOfType<GameManager>().PlayerScore());
            GetLeaderboard();
        }
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "High Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent");
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "High Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in entryContainer)
        {
            Destroy(item.gameObject);
        }

        int debugCounter = 0;
        foreach (var item in result.Leaderboard)
        {
            
            Debug.Log("Getting:" +debugCounter);
            GameObject gameObject = Instantiate(rowPrefab, entryContainer);
            Text[] texts = gameObject.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.StatValue.ToString();
            texts[2].text = item.Profile.DisplayName; //item.PlayFabId;
            debugCounter++;
        }
    }

    public void GetName()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),  OnDataRecieved, OnError);
    }

    private void OnDataRecieved(GetUserDataResult result)
    {
        string name = "";
        Debug.Log("Recieved");
        if (result.Data != null && result.Data.ContainsKey("Username"))
        {
            name = result.Data["Username"].Value;
        }
    }

    public void SaveName(string name)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Username", name}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void OnDataSend(UpdateUserDataResult obj)
    {
        Debug.Log("Data send failed");
    }

    
    public void SubmitDisplayName(string s)
    {
        if (s == "" || s == " " || s == null)
        {
            //name = "TheBigDig";
            name = Random.Range(10000000, 99999999).ToString();
        }

        if (name.Length > 10)
        {
            name = name.Substring(0, 10);
        }
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = s
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        this.gameObject.SetActive(true);
        GetLeaderboard();
    }
}
