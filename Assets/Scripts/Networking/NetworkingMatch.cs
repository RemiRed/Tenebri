using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkingMatch : NetworkBehaviour
{
    public MatchInfoSnapshot matchInfoSnapshot = new MatchInfoSnapshot();
    public MatchInfo matchInfo = new MatchInfo();
    [SerializeField]
    NetworkingLobby networkManager;

    [SerializeField]
    bool matchList = true;

    private void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkingLobby>();
    }
    private void Update()
    {
        if (matchList)
        {
            GetComponentInChildren<Text>().text = matchInfoSnapshot.name;
        }
    }

    public void JoinMatch()
    {
        networkManager.MatchmakingJoinMatch(matchInfoSnapshot);
    }

    public void DestroyMatch()
    {
        networkManager.MatchmakingDestroyMatch(matchInfo);
    }
}
