using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHUD : NetworkBehaviour
{
    [SerializeField]
    GameObject buttonLANHost, buttonLANJoin, textboxLANJoin;
    NetworkManager networkManager;

    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
      //  Instantiate(buttonLANHost);
    }

    public void LANHost()
    {
        print("Host");
        networkManager.StartHost();
    }

    public void LANJoin()
    {
        print("Join");
        networkManager.networkAddress = "localhost";
        networkManager.StartClient();
    }
}
