using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkingLobby : NetworkLobbyManager
{
    void Start()
    {
        MatchmakingStart();
        MatchmakingListMatches();
    }

    void MatchmakingStart()
    {
        print("@ MatchmakingStart");
        StartMatchMaker();
    }

    void MatchmakingListMatches()
    {
        print("@ MatchmakingListMatches");
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
                print("Listed matches. 1st match: " + matchList[0]);
                MatchmakingJoinMatch(matchList[0]);
            }
            else
            {
                MatchmakingCreateMatch("MM");
            }
        }
    }

    void MatchmakingJoinMatch(MatchInfoSnapshot firstMatch)
    {
        print("@ MatchmakingJoinMatch");
        matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
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

    void MatchmakingCreateMatch(string matchName)
    {
        print("@ MatchmakingCreateMatch");
        matchMaker.CreateMatch(matchName, 15, true, "", "", "", 0, 0, OnMatchCreate);
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
            print("Successfully created match: " + matchInfo.networkId);  
        }
    }
}
