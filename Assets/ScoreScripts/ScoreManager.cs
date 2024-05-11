using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _score = 0;

    private void Awake()
    {
        SetScore(0);
    }

    // Start is called before the first frame update
    //private void Start()
    //{
    //    _scoreText.text = _score.ToString() + " POINTS";
    //}

    private void SetScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString() + " POINTS";
    }

    public void UpdateScore(Component sender, object data)
    {
        if (data is int)
        {
            int _amount = (int)data;
            SetScore(_amount);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}