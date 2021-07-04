using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Event/Game Event Data")]
public class GameEventSys : ScriptableObject
{
    private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

    public void Raise()
    {
        foreach (var eventListener in listeners)
        {
            eventListener.OnCallEvent();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
