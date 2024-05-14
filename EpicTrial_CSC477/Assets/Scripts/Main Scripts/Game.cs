using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static Game Instance { get; private set; }
    public Input input;

    private WaveManager waveManager;

    // Start is called before the first frame update
    void Start() {
        input = new Input();
        input.Enable();
        Instance = this;

        waveManager = FindObjectOfType<WaveManager>(); 
        waveManager.OnAllWavesCompleted.AddListener(ProgressToNextScene); 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        if (enemies.Length == 0 && bosses.Length == 0)
        {
            ProgressToNextScene();
        }
        */
    }

    void ProgressToNextScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
