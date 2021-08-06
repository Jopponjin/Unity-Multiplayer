using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Ui Data", menuName = "Core System Data/UI Data")]
public class UiCoreData : ScriptableObject
{
    public enum UiState
    {
        None,
        Menu,
        InGame
    }
    public  UiState currentUiState { get; set; }

    public GameObject mainMenuRef { get; set; }
    public GameObject hostMenuRef { get; set; }
    public GameObject inGameMenuRef { get; set; }

    public  InputField joinPasswordField { get; set; }
    public  InputField hostPasswordField { get; set; }
    public  Toggle hostPasswordToggle { get; set; }
}
