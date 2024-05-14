using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleRoom : MonoBehaviour
{

    public LaserTurret T1;
    public LaserTurret T2;
    public LaserTurret T3;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if( !T1.GetActive() &&  !T2.GetActive() && !T3.GetActive())
        {
            ProgressToNextScene();
        }
    }

    void ProgressToNextScene()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
