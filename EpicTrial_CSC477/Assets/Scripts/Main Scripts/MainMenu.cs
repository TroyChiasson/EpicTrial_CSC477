using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;
    public GameObject ControlsMenu;

    void Start()
    {
        SettingsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Round1");
    }   

    public void Quit()
    {
        Application.Quit();
    }
}
