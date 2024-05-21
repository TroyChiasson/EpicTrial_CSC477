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
            if (dropChoice > .05f && dropChoice <= 1f) {
                HealthUp();
            }
            if (dropChoice <= .05f) {
                this.gameObject.transform.position = new Vector3(1000f, 1000f, 1000f);
                StartCoroutine(Invulnerability());
            }
        }
    }

    void HealthUp() {
        if (playerHealth < 3) {
            playerHealth++;
        }
        Destroy(this.gameObject);
    }

    IEnumerator Invulnerability() {
        invulnerable = true;
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
        invulnerable = false;
    }
}
