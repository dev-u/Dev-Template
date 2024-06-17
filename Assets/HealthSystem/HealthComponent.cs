using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HealthComponent is the component attached to an entity that has a healthSystem
public class HealthComponent : MonoBehaviour
{
    // Reference to GameEvent to trigger
    [SerializeField] private GameEvent _healthChanged;
    [SerializeField] private int maxHealth;
    
    private HealthSystem _healthSystem;

    private void Start()
    {
        // Sets Up the healthSystem
        _healthSystem = new HealthSystem(maxHealth);

        // Sends the event at the start of the game to set the displayer as well.
        _healthChanged.Raise(this, _healthSystem.GetHealthPercentage());
    }

    // ChangeHealth is the mediator between the HealthSystem and the HealthDisplay
    public void ChangeHealth(int amount)
    {
        _healthSystem.UpdateHealth(amount);

        float percentageHealth = _healthSystem.GetHealthPercentage();
        _healthChanged.Raise(this, percentageHealth);
    }
}
