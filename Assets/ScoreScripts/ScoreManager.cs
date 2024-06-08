using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    // Reference Component to text display
    [SerializeField]
    private TMP_Text _scoreText;

    private int _score = 0;

    // SetScore is called in Awake to restart the score
    private void Awake()
    {
        SetScore(0);
    }

    // SetScore can be called when the score changes
    private void SetScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString() + " POINTS";
    }

    // Using GameEvents, ScoreManager listens to an event and calls UpdateScore
    public void UpdateScore(Component sender, object data)
    {
        if (data is int)
        {
            int _amount = (int)data;
            SetScore(_amount);
        }
    }
}