using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private SceneAsset startGame;
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void BeginGame()
    {
        SceneManager.LoadScene(startGame.name);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ChangeSelected()
    {
        button.Select();
    }
}
