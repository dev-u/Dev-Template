using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int _maxHealth;
    private int _currentHealth;

    // This constructor makes it possible to setup the health when creating it.
    public HealthSystem(int maxValue)
    {
        _maxHealth = maxValue;
        _currentHealth = _maxHealth;
    }

    // The constructor also has an override to make it possible to start at any amount of health.
    // Can also be used to update the maxHealth while mantaining the currentHealth.
    public HealthSystem(int maxValue, int currentValue)
    {
        _maxHealth = maxValue;
        _currentHealth = currentValue;
    }

    // GetHealth returns the entity's currentHealth.
    public int GetHealth()
    {
        return _currentHealth;
    }

    // GetHealthPercentage returns a numbern between 0 and 1, corresponding to the currentHealth in relation to maxHealth.
    public float GetHealthPercentage()
    {
        return (float)_currentHealth / _maxHealth;
    }

    
    // UpdateHealth is called to either heal or damage the entity.
    // Sending a positive number as parameter heals the entity, and sending a negative number hurts it.
    public void UpdateHealth(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        else if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
