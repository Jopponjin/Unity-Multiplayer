using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;

public class NetworkingConnection : MonoBehaviour
{
    private void Setup()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost();
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, MLAPI.NetworkManager.ConnectionApprovedDelegate callback)
    {
        //Your logic here
        bool approve = true;
        bool createPlayerObject = true;

        // The prefab hash. Use null to use the default player prefab
        // If using this hash, replace "MyPrefabHashGenerator" with the name of a prefab added to the NetworkPrefabs field of your NetworkManager object in the scene
        ulong? prefabHash = NetworkSpawnManager.GetPrefabHashFromGenerator("MyPrefabHashGenerator");

        //If approve is true, the connection gets added. If it's false. The client gets disconnected
        //callback(createPlayerObject, prefabHash, approve, positionToSpawnAt, rotationToSpawnWith);
    }
}