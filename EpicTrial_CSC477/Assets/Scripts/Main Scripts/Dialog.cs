using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DialogIntro : MonoBehaviour
{

    public Input input;

    public TMP_Text speakerText;
    public TMP_Text dialogText;
    public Image portraitImage;


    // Dialog Content
    public string[] speaker;
    [TextArea]
    public string[] dialogWords;
    public Sprite[] portrait;

    private int currentDialogIndex = 0;
    private int currentCharIndex = 0;
    private bool isDialogComplete = false;


    void Start()
    {
        input = new Input();
        input.Enable();
        StartCoroutine(ShowDialog());
    }

    void Update()
    {
        // Check if the button is not pressed and dialog is complete
        if (input.Default.Space.WasPressedThisFrame())
        {
            NextDialog();
        }
    }

    IEnumerator ShowDialog()
    {
        float characterDelay = 0.05f; // Adjust the delay between characters

        while (currentDialogIndex < dialogWords.Length)
        {
            if (!isDialogComplete)
            {
                if (currentCharIndex < dialogWords[currentDialogIndex].Length)
                {
                    // Display text character by character with delay
                    dialogText.text = dialogWords[currentDialogIndex].Substring(0, currentCharIndex + 1);
                    currentCharIndex++;
                    Debug.Log("Displaying character: " + currentCharIndex);
                    yield return new WaitForSeconds(characterDelay); // Introduce delay between characters
                }
                else
                {
                    // Text display is complete for current dialog
                    isDialogComplete = true;
                    Debug.Log("Dialog complete");
                }
            }
            else
            {
                // Wait for "Q" input to proceed to the next dialog
                if (input.Default.Space.WasPressedThisFrame()) {
                    NextDialog();
                }
            }

            yield return null;
        }
    }


    // Method to advance to the next dialog
    void NextDialog()
    {
        currentDialogIndex++;
        currentCharIndex = 0;

        Debug.Log(currentDialogIndex);

        if (currentDialogIndex < dialogWords.Length)
        {
            // Display next dialog
            dialogText.text = "";
            speakerText.text = speaker[currentDialogIndex];
            portraitImage.sprite = portrait[currentDialogIndex];
            isDialogComplete = false; // Reset dialog completion flag
        }
        else
        {
            // Dialog is complete
            ProgressToNextScene();
        }
    }

    void ProgressToNextScene()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}