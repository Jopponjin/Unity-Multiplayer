using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField] NetCoreData netCoreData;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(false);
            gameObject.GetComponent<MovmentData>().canMove = false;
        }
        else
        {
            gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(true);
            gameObject.GetComponent<MovmentData>().canMove = true;
        }
    }
}
