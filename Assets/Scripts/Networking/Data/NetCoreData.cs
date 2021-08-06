using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Netwokring Data", menuName = "Core System Data/ Network Data")]
public class NetCoreData : ScriptableObject
{
    public bool shouldHost {  get; set; }
    public bool shouldJoin {  get; set; }

    public  bool IsPasswordProtected { get; set; }

    public string hostedServerPassowrd { get; set; }

    public string currentWritenPassword;

    public string writtenIpAddress;

    public enum NetClientState
    {
        Menu,
        Host,
        Client
    }
    public NetClientState currentNetState;
}
