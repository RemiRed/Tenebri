using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkingMatch : NetworkBehaviour
{
    public MatchInfoSnapshot matchInfo = new MatchInfoSnapshot();

    private void Update()
    {
        GetComponentInChildren<Text>().text = matchInfo.name;
    }
}
