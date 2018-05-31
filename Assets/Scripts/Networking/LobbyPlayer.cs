using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer
{
    //By Andreas Halldin
    //Handles the ready button for the lobby player, inherits from networkLobbyPlayer so that both scripts aren't needed on one object
    [SerializeField]
    Button readyButton; //The ready button

    private void Start() //Find the ready button
    {
        DontDestroyOnLoad(gameObject);
        readyButton = GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>();
        readyButton.onClick.AddListener(Ready);
    }

    public void Ready() //Toggle ready/not ready
    {
        if (!readyToBegin)
        {
            if (isLocalPlayer)
                SendReadyToBeginMessage();
        }
        else
        {
            SendNotReadyToBeginMessage();
        }
    }
}