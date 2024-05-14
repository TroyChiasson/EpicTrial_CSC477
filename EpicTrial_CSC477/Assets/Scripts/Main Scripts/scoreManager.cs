using HighScore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;
    public TextMeshProUGUI scoreText;

    public static int score = 1903;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
