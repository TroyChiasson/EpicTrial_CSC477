using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public LaserTurret lt;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            lt.ToggleInLaser();
        }
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Enemy") {
            lt.AddDestroyList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player") {
            lt.ToggleInLaser();
        }
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Enemy") {
            lt.RemoveDestroyList(other.gameObject);
        }
    }
}
