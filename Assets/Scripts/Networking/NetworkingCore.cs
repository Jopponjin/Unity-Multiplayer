using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using System.Text;
using System;

public class NetworkingCore : NetworkBehaviour
{
    [SerializeField] UiCoreData uiData;
    [SerializeField] CoreData coreData;
    [SerializeField] NetCoreData netCoreData;

    Ping ping;

    private void Start()
    {
        
    }

    void ApplyIngameDataValues()
    {
        netCoreData.currentNetState = NetCoreData.NetClientState.Client;

        if (netCoreData.shouldHost) HostEvent();
        else if(netCoreData.shouldJoin) CallClientEvent();

    }

    void ApplyMenuDataValues()
    {
        netCoreData.currentNetState = NetCoreData.NetClientState.Menu;

        netCoreData.hostedServerPassowrd = null;
        netCoreData.IsPasswordProtected = false;

        netCoreData.shouldHost = false;
        netCoreData.shouldJoin = false;
    }

    private void OnDestroy()
    {
        // Prevent error in the editor
        if (NetworkManager.Singleton == null) { return; }

        NetworkManager.Singleton.OnServerStarted -= HostEvent;
        NetworkManager.Singleton.OnClientConnectedCallback -= ClientConnectEvent;
        LeaveEvent();
    }

    public void HostEvent()
    {
        // Hook up password approval check
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(Vector3.zero, Quaternion.Euler(0,0,0), true, null);
    }

    public void CallClientEvent()
    {
        ulong m_longtemp = 0;

        ClientConnectEvent(m_longtemp);
    }

    public void ClientConnectEvent(ulong m_someting)
    {
        // Set password ready to send to the server to validate
        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(uiData.joinPasswordField.text);

        //NetworkManager.Singleton.NetworkConfig.

        NetworkManager.Singleton.StartClient();
    }

    public void LeaveEvent()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StopHost();
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StopClient();
        }
    }


    private void ApprovalCheck(byte[] connectionData, ulong clientId, MLAPI.NetworkManager.ConnectionApprovedDelegate callback)
    {
        string password = Encoding.ASCII.GetString(connectionData);

        callback(true, null, true, Vector3.zero, Quaternion.Euler(0, 0, 0));

        if (netCoreData.IsPasswordProtected)
        {
            bool approveConnection = password == netCoreData.currentWritenPassword;
            if (approveConnection)
            {
                callback(true, null, approveConnection, Vector3.zero, Quaternion.Euler(0, 0, 0));
            }

        }
        else if (!netCoreData.IsPasswordProtected)
        {
            callback(true, null, true, Vector3.zero, Quaternion.Euler(0, 0, 0));
        }
    }

}
