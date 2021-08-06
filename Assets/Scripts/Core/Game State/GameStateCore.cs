using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateCore : MonoBehaviour
{
    [SerializeField] CoreData coreData;
    [SerializeField] NetCoreData netCoreData;
    [Space]
    [SerializeField] GameEventSys connectGameEvent;
    [SerializeField] GameEventSys hostGameEvent;
    [SerializeField] GameEventSys quitGameEvent;

    private void Start()
    {
        coreData.currentGameScene = SceneManager.GetActiveScene();

        SetGameState();

        if (netCoreData.currentNetState == NetCoreData.NetClientState.Client)
        {
            //coreData.cameraObjects = GameObject.FindGameObjectsWithTag("Player");

            //for (int i = 0; i < coreData.cameraObjects.Length; i++)
            //{
            //    if (coreData.cameraObjects[i].GetComponentInChildren<Camera>())
            //    {
            //        coreData.cameraObjects[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
            //    }
            //}
        }
    }

    void SetGameState()
    {
        if (coreData.currentGameScene == SceneManager.GetSceneByName("Menu"))
        {
            coreData.currentGameState = CoreData.InGameState.Menu;
        }
        else if (coreData.currentGameScene == SceneManager.GetSceneByName("Level 1"))
        {
            Debug.Log(coreData.currentGameScene.name);

            coreData.currentGameState = CoreData.InGameState.InGame;
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }

    public void JoinGame()
    {
        netCoreData.shouldJoin = true;
        netCoreData.shouldHost = false;

        coreData.currentGameState = CoreData.InGameState.InGame;

        ChangeScene();
    }

    public void HostGame()
    {
        netCoreData.shouldJoin = false;
        netCoreData.shouldHost = true;

        coreData.currentGameState = CoreData.InGameState.InGame;
    }

    public void QuitGame()
    {
        Debug.Log("[ CORE ]: 'QuitGame()' called.");

        Application.Quit();

        //if (coreData.currentGameState == CoreData.InGameState.InGame)
        //{
        //    SceneManager.LoadScene("Menu");
        //    coreData.currentGameState = CoreData.InGameState.Menu;

        //    for (int i = 0; i < coreData.gameScenes.Count; i++)
        //    {
        //        if (coreData.gameScenes[i].name == "Menu")
        //        {
        //            coreData.currentGameScene = coreData.gameScenes[i];
        //        }
        //    }
        //}    
        //else if (coreData.currentGameState == CoreData.InGameState.Menu)
        //{
        //    Application.Quit();
        //}
    }
}
