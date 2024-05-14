using HighScore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    private void Start()
    {
        HS.Init(this, "Momento Mori");
    }

    public void SubmitScore()
    {
        HS.SubmitHighScore(this, playerNameText.text, scoreManager.score);
    }
    
    public void ReturnMainMenu()
    {
        SubmitScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
