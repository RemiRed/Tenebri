using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkingMatch : NetworkBehaviour
{
    //By Andreas Halldin
    //UI Element representing a match

    public MatchInfoSnapshot matchInfoSnapshot = new MatchInfoSnapshot();
    public MatchInfo matchInfo = new MatchInfo();
    [SerializeField]
    NetworkingLobby networkManager;

    [SerializeField]
    bool matchList = true;

    private void Start() //Set network Manager
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkingLobby>();
    }
    private void Update() //Show name of match
    {
        if (matchList)
        {
            GetComponentInChildren<Text>().text = matchInfoSnapshot.name;
        }
    }

    public void JoinMatch() //Join a match
    {
        networkManager.MatchmakingJoinMatch(matchInfoSnapshot);
    }

    public void DestroyMatch() //Destroy a match
    {
        networkManager.MatchmakingDestroyMatch(matchInfo);
    }
}
