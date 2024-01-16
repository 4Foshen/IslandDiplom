using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" 
            || SceneManager.GetActiveScene().name == "StoryScene" 
            || SceneManager.GetActiveScene().name == "EndingScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }
    }
    public void ChangeLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ExitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
