using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class InGameMenu : MonoBehaviour
{
    public GameObject ServerDebug;
    [Space]
    public GameEventSys hostGameEvent;
    public GameEventSys connectGameEvent;
    public GameEventSys disconnectGameEvent;
    [Space]
    public Button hostButton;
    public Button connectButton;
    public Button disconnectButton;

    // Start is called before the first frame update
    void Awake()
    {
        ServerDebug = GameObject.Find("Server Debug");
    }

    private void Update()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            hostButton.enabled = true;
            connectButton.enabled = true;
            disconnectButton.enabled = false;
        }
        else
        {
            hostButton.enabled = false;
            connectButton.enabled = false;
            disconnectButton.enabled = true;
        }
    }

    public void ConnectToServer()
    {
        connectGameEvent.Raise();
    }

    public void HostServer()
    {
        hostGameEvent.Raise();
    }

    public void DisconnectedFromServer()
    {
        disconnectGameEvent.Raise();
    }


}
