using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer
{
    [SerializeField]
    Button readyButton;
    private void Start()
    {
        //   readyButton = GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>();
        readyButton.onClick.AddListener(Ready);
    }

    public void Ready()
    {
        readyToBegin = !readyToBegin;
    }
}
