using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour {

    NetworkingLobby networkingLobby;
    private void Start()
    {
        networkingLobby = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkingLobby>();
    }

    public void Menu()
    {
        networkingLobby.StopMatchMaker();
        GameObject newGO = new GameObject();
        networkingLobby.gameObject.transform.parent = newGO.transform;
        networkingLobby.StopHost();
    }
}
