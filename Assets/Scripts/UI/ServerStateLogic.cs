using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class ServerStateLogic : MonoBehaviour
{
    NetworkManager networkManager;
    // Start is called before the first frame update
    void Awake()
    {
        networkManager = GetComponent<NetworkManager>();

        networkManager.SetSingleton();
    }

    public void HostServer()
    {
        Debug.Log("Host server event called!");
        networkManager.StartHost();

    }

    public void ConnectedToServer()
    {
        Debug.Log("Connect to server event called!");
        //TODO: make a system to connect to servers by ip.
        //NetworkManager.Singleton.

    }

    public void DisconnectFromServer()
    {
        Debug.Log("Disconnect from server event called!");
        //TODO: Make a system to disconnect clients on the side and kick funtion
        //NetworkManager.Singleton.DisconnectClient();
    }
}
