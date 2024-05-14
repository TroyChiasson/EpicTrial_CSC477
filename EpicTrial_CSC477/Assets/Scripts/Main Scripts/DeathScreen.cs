using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private void Start()
    {
        HS.Init(this, "Troy's Game");
    }
    public void ReturnMainMenu()
    {
        HS.SubmitHighScore(this, "Troy Himself", scoreManager.score);
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
