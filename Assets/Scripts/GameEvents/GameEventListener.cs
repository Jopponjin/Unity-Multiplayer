using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEventSys gameEvent;

    [SerializeField]
    private UnityEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnCallEvent()
    {
        response.Invoke();
    }
}
