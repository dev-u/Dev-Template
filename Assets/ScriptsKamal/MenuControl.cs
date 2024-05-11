using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] SceneAsset startGame;

    public void BeginGame()
    {
        SceneManager.LoadScene(startGame.name);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
