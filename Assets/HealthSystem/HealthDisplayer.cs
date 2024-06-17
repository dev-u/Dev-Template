using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class HealthDisplayer : MonoBehaviour
{
    private Transform _bar;

    void Awake()
    {
        // The Bar's transform is found by name.
        // If you want to change the way it is displayed, don't forget to match the gameObject's name with this.
        _bar = transform.Find("Bar");
    }

    // This is the function used by GameEventListener. If you are not using GameEvents, use ChangeScale directly.
    public void OnHealthChangedEvent(Component sender, object data)
    {
        if (data != null && data.GetType() == typeof(float))
        {
            float newData = (float)data;

            ChangeScale(newData);
        }
    }

    // ChangeScale changes the HealthBar.
    public void ChangeScale(float newScale)
    {
        _bar.localScale = new Vector3 (newScale, 1f, 1f);
    }
}
