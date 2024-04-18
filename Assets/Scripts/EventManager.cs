using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class that extends UnityEvent
public class MoveEvent : UnityEvent<Vector3>
{

}

// Event manager class - not attached to game object
public class EventManager
{
    private static EventManager instance;
    private Dictionary<string, MoveEvent> events;

    private MoveEvent wordEvent;

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;
        }
    }

    public void RegisterToMoveEvent(UnityAction<Vector3> listener)
    {
        if (wordEvent == null)
            wordEvent = new MoveEvent();

        wordEvent.AddListener(listener);
    }

    public void TriggerMoveEvent(Vector3 targetPosition)
    {
        wordEvent.Invoke(targetPosition);
    }
}