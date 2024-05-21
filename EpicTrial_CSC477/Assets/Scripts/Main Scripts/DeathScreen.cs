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
        HS.Init(this, "Memento Mori");
    }

    public void SubmitScore()
    {
        // CHECK HIGHSCORE HERE
        // https://www.protohacks.net/LATech/499/scores/

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
