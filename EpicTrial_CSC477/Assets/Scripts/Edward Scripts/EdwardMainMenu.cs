using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EdwardMainMenu : MonoBehaviour
{
    public GameObject SettingsMenu;

    void Start()
    {
        SettingsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Edward Scene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
