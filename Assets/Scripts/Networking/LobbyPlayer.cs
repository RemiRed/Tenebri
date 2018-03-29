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
        readyButton.onClick.AddListener(Ready);
        closeButton.onClick.AddListener(Remove);
    }

    public void Ready()
    {
        readyToBegin = !readyToBegin;
    }
    public void Remove()
    {
        RemovePlayer();
    }
}

