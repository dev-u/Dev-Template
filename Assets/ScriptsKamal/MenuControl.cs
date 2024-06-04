using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private SceneAsset startGame; //scene to start the game
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void BeginGame()
    {
        SceneManager.LoadScene(startGame.name); //start the scene in the Field
    }
    public void Exit()
    {
        Application.Quit(); //exit application
    }
    public void ChangeSelected()
    {
        button.Select(); //change the selected button (used in Event Trigger to change when the mouse pass over the button)
    }
}
