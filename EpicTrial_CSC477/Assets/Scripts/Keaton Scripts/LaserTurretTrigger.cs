using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretTrigger : MonoBehaviour {

    public LaserTurret lTurret;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            lTurret.ToggleFire();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player") {
            lTurret.ToggleFire();
        }
    }
}
