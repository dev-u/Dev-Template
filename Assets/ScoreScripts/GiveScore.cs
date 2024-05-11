using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveScore : MonoBehaviour
{
    [SerializeField]
    private int _scoreValue;

    [Header("Events")]
    public GameEvent _onScoreChanged;

    public void ChangeScore()
    {
        _onScoreChanged.Raise(this, _scoreValue);
    }
}