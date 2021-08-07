using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCore : MonoBehaviour
{
    [SerializeField] CoreData coreData;
    [SerializeField] UiCoreData uiData;
    [SerializeField] NetCoreData netData;
    [Space]

    [SerializeField] InputField connectionPasswordField;
    [Space]

    [SerializeField] InputField hostUIInputfieldObj;
    [SerializeField] Toggle hostUiToggleObj;
    [Space]

    [SerializeField] GameObject inGameMenuObject;
    [SerializeField] GameObject clientJoinMenuObject;

    void Awake()
    {
        SetUiDataValues();
    }

    private void Update()
    {
        if (coreData.currentGameState == CoreData.InGameState.InGame)
        {
            InGameMenuNavigation();
        }
        
    }

    private void SetUiDataValues()
    {
        uiData.hostPasswordField = hostUIInputfieldObj;
        uiData.hostPasswordToggle = hostUiToggleObj;

        uiData.inGameMenuRef = inGameMenuObject;
        uiData.clientJoinMenuRef = clientJoinMenuObject;
        uiData.joinPasswordField = connectionPasswordField;
    }

    public void SetPasswordProtected()
    {
        if (hostUiToggleObj.isOn) netData.IsPasswordProtected = true;
        else if (!hostUiToggleObj) netData.IsPasswordProtected = false;
    }
    
    public void HideAllUiObjects()
    {
        uiData.hostMenuRef.SetActive(false);
        uiData.mainMenuRef.SetActive(false);
    }

    void InGameMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!inGameMenuObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                inGameMenuObject.SetActive(true);

                Debug.Log("[ CORE ]: Mouse is unlocked!");
            }
            else if (inGameMenuObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                inGameMenuObject.SetActive(false);
                Debug.Log("[ CORE ]: Mouse is locked!");
            }
        }
    }
}
