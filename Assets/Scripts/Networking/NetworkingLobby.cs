﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class NetworkingLobby : NetworkLobbyManager
{
    [SerializeField]
    GameObject matchmakingGameObject, optionsGameObject, exitGameObject, scrollView, scrollViewContent, matchPanel, createMatchPanel, closeMatchButton, createMatchButton, playerCount;

    [SerializeField]
    InputField matchInputField;
    [SerializeField]
    GameObject matchUIPrefab;
    List<GameObject> matchUIList = new List<GameObject>();

    [SerializeField]
    int delay = 500;
    int currentDelay;

    bool matchListBool = false;
    bool inMatch = false;

    private void Update()
    {
        if (matchListBool)
        {
            currentDelay--;
            if (currentDelay <= 0)
            {
                currentDelay = delay;
                MatchmakingListMatches();
            }
        }
        if (inMatch)
        {
            PlayerCount();
        }
    }

    public void MatchmakingStart()
    {
        print("@ MatchmakingStart");
        StartMatchMaker();
        MatchmakingListMatches();
        matchmakingGameObject.SetActive(false);
        optionsGameObject.SetActive(false);
        exitGameObject.SetActive(false);
    }

    void MatchmakingListMatches()
    {
        print("@ MatchmakingListMatches");
        scrollView.SetActive(true);
        currentDelay = delay;
        createMatchPanel.SetActive(true);
        matchListBool = true;
        foreach (GameObject matchUI in matchUIList)
        {
            Destroy(matchUI);
        }
        matchUIList.Clear();
        matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        print("@ OnMatchList");
        base.OnMatchList(success, extendedInfo, matchList);

        if (!success)
        {
            print("List Failed: " + extendedInfo);
        }
        else
        {
            if (matchList.Count > 0)
            {
                print(matchList.Count);
                foreach (MatchInfoSnapshot match in matchList)
                {
                    GameObject matchUIGO = Instantiate(matchUIPrefab, scrollViewContent.transform);
                    matchUIGO.transform.localPosition = new Vector3(matchUIGO.transform.localPosition.x, -15 - (30 * matchUIList.Count + 1), matchUIGO.transform.localPosition.z);
                    matchUIGO.GetComponent<NetworkingMatch>().matchInfoSnapshot = match;
                    matchUIList.Add(matchUIGO);
                }
            }
        }
    }


    public void MatchmakingJoinMatch(MatchInfoSnapshot match)
    {
        print("@ MatchmakingJoinMatch");
        matchListBool = false;
        inMatch = true;
        scrollView.SetActive(false);
        matchPanel.SetActive(true);
        createMatchPanel.SetActive(false);
        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        print("@ OnMatchJoined");
        base.OnMatchJoined(success, extendedInfo, matchInfo);

        if (!success)
        {
            print("Failed to join match: " + extendedInfo);
        }
        else
        {
            print("Successfully joined match: " + matchInfo.networkId);
        }
    }

    public void MatchmakingCreateMatch()
    {
        string matchName = matchInputField.text;
        if (matchName != null && matchName != "")
        {
            print("@ MatchmakingCreateMatch");
            matchListBool = false;
            inMatch = true;
            scrollView.SetActive(false);
            matchPanel.SetActive(true);
            createMatchPanel.SetActive(false);
            matchMaker.CreateMatch(matchName, 2, true, "", "", "", 0, 0, OnMatchCreate);
        }
        else
        {
            //TODO Feedback : ENTER NAME OF MATCH
        }
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        print("@ OnMatchCreate");
        base.OnMatchCreate(success, extendedInfo, matchInfo);

        if (!success)
        {
            print("Failed to create match: " + extendedInfo);
        }
        else
        {
            print(matchInfo.nodeId + " - Node ID");
            closeMatchButton.GetComponent<NetworkingMatch>().matchInfo = matchInfo;
            print("Successfully created match: " + matchInfo.networkId);
        }
    }

    public void MatchmakingDestroyMatch(MatchInfo matchInfo)
    {
        print("@ MatchmakingCloseMatch");
        matchPanel.SetActive(false);
        matchmakingGameObject.SetActive(true);
        optionsGameObject.SetActive(true);
        exitGameObject.SetActive(true);
        inMatch = false;
        StopMatchMaker();
        GameObject newGO = new GameObject();
        transform.parent = newGO.transform;
        StopHost();
    }

    public void PlayerCount()
    {
        if (playerCount == null)
        {
            inMatch = false;
            return;
        }
        int playerCountInt = 0;
        int readyCountInt = 0;
        foreach (NetworkLobbyPlayer nlp in lobbySlots)
        {
            if (nlp != null)
            {
                playerCountInt++;
                if (nlp.readyToBegin)
                {
                    readyCountInt++;
                }
            }

        }
        playerCount.GetComponent<Text>().text = "Player Count: " + playerCountInt + "\nReady: " + readyCountInt;
    }
}
