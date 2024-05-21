using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static Game Instance { get; private set; }
    public Input input;
    [SerializeReference] public Material LowHealthMaterial;

    private WaveManager waveManager;

    // Start is called before the first frame update
    void Start() {
        input = new Input();
        input.Enable();
        Instance = this;

        waveManager = FindObjectOfType<WaveManager>(); 
        waveManager.OnAllWavesCompleted.AddListener(ProgressToNextScene);
        LowHealthMaterial.SetFloat("_LowHealth", 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainPlayer.playerHealth == 1)
        {
            LowHealthMaterial.SetFloat("_LowHealth",0.3f);
        }
        else
        {
            LowHealthMaterial.SetFloat("_LowHealth", 0.0f);
        }
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
