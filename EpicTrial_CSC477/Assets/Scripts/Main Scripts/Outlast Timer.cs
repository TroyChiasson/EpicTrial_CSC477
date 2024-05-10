using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OutlastTimer : MonoBehaviour
{
    public float countdownTime = 20f; 
    private TextMeshProUGUI timerText;
    public GameObject shieldObject;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            shieldObject.SetActive(false);

            // Update the timer text to display minutes and seconds
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f); // Wait for 1 second
            currentTime -= 1f; // Decrease the current time by 1 second
        }

        if (currentTime <= 0)
        {
            shieldObject.SetActive(true);
            ProgressToNextScene();
        }
        
        // When the countdown is finished, display "0:00"
        timerText.text = "0:00";
    }

    void ProgressToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
