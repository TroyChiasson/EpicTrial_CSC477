using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public GameObject[] waveObjects; 
    private int currentWaveIndex = 0;

    public UnityEvent OnAllWavesCompleted; 
    public float delayBeforeCheck = 3f; 
    private bool canCheckEnemies = true;

    private void Start()
    {
        // Set all waves inactive except the first one
        SetAllWavesInactive();
        ActivateWave(0);
    }

    private void Update()
    {
        if (canCheckEnemies)
        {
            delayBeforeCheck -= Time.deltaTime;

            if (delayBeforeCheck <= 0f)
            {
                // Check for Enemy
                if (!CheckForEnemies())
                {
                    if (currentWaveIndex < waveObjects.Length - 1)
                    {
                        currentWaveIndex++;
                        ActivateWave(currentWaveIndex);
                    }
                    else
                    {
                        //Invoke flag
                        OnAllWavesCompleted.Invoke();
                    }
                }
                delayBeforeCheck = 3f;
            }
        }
    }

    private void SetAllWavesInactive()
    {
        foreach (GameObject wave in waveObjects)
        {
            wave.SetActive(false);
        }
    }

    private void ActivateWave(int index)
    {
        //Set the current wave active
        waveObjects[index].SetActive(true);

        //Reset the flag to check enemies
        canCheckEnemies = true;
    }

    private bool CheckForEnemies()
    {
        return GameObject.FindGameObjectWithTag("Enemy") != null || GameObject.FindGameObjectWithTag("Boss") != null;
    }
}
