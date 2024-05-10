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

    void HealthUp() {
        playerHealth++;
        Destroy(this.gameObject);
    }
}