using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CHECK HIGHSCORE HERE
// https://www.protohacks.net/LATech/499/scores/
public class HighScoreTest : MonoBehaviour
{
    // private float delay;
    // Start is called before the first frame update
    void Start()
    {
        HS.Init(this, "Troy's Game");
        // delay = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // if (delay > 0) {
        //     delay -= Time.deltaTime;
        //     if (delay < 0) {
        //         HS.SubmitHighScore(this, "Troy Himself", Random.Range(100,1000));
        //     }
        // }
    }


}
