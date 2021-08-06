using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] UiCoreData uiData;

    public GameObject ServerDebug;
    [Space]
    public GameEventSys hostGameEvent;
    public GameEventSys connectGameEvent;
    public GameEventSys disconnectGameEvent;
    [Space]
    public Button disconnectButton;

    // Start is called before the first frame update
    void Awake()
    {
        ServerDebug = GameObject.Find("Server Debug");
    }

    private void Update()
    {
        if (uiData.currentUiState == UiCoreData.UiState.InGame)
        {
            
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
