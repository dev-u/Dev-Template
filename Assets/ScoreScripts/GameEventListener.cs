using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object>
{ }

public class GameEventListener : MonoBehaviour
{
    public GameEvent _gameEvent;
    public CustomGameEvent _response;

    private void OnEnable()
    {
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        _gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        _response.Invoke(sender, data);
    }
}