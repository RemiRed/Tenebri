using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    //By Andreas Halldin
    //Handles the Game over menu

    NetworkingLobby networkingLobby;
    private void Start() //Find the network Manager
    {
        networkingLobby = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkingLobby>();
    }

    public void Menu() //Back to menu
    {
        networkingLobby.StopMatchMaker();
        GameObject newGO = new GameObject();
        networkingLobby.gameObject.transform.parent = newGO.transform;
        networkingLobby.StopHost();
    }
}
