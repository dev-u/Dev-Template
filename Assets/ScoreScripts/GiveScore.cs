using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScore : MonoBehaviour
{
    [SerializeField]
    private int _scoreValue;

    [Header("Events")]
    public GameEvent _onScoreChanged;

    // ChangeScore is called to send an event for ScoreManager to hear
    public void ChangeScore()
    {
        _onScoreChanged.Raise(this, _scoreValue);
    }
}