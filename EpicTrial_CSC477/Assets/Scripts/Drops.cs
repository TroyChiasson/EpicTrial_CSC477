using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MainPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MainPlayer") {
            float dropChoice = Random.value;
            if (dropChoice <= 1) {
                HealthUp();
            }
        }
    }

    IEnumerator SpeedUp() { // Use StartCoroutine(SpeedUp()); to use it.
        MainPlayer.playerSpeed = 10f;
        print("Hi");
        yield return new WaitForSeconds(5f);
        print("bye");
        MainPlayer.playerSpeed = 5f;
        Destroy(this.gameObject);
    }

    void HealthUp() {
        playerHealth++;
        UpdateHealthUI();
        Destroy(this.gameObject);
    }
}
