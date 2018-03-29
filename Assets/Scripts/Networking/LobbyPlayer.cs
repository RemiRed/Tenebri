using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer
{
    [SerializeField]
    Button readyButton, closeButton;
    private void Start()
    {
        readyButton = GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>();
        closeButton = GameObject.FindGameObjectWithTag("CloseButton").GetComponent<Button>();
        readyButton.onClick.AddListener(CmdReady);
        //closeButton.onClick.AddListener(Remove);
    }
    [Command]
    public void CmdReady()
    {
        readyToBegin = !readyToBegin;
    }
    public void Remove()
    {
        RemovePlayer();
        closeButton.GetComponent<NetworkingMatch>().DestroyMatch();
    }
}

