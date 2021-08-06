using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Core Data", menuName = "Core System Data/Core Data")]
public class CoreData : ScriptableObject
{

    public enum InGameState
    {
        Start,
        Menu,
        InGame
    }
    public InGameState currentGameState;

    public List<Scene> gameScenes = new List<Scene>();

    public Scene currentGameScene;

    public string sceneSelected;

    // ----- Scene Objects -----//

    public GameObject localPlayerObj;

    public GameObject[] cameraObjects;

}
