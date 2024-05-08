using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurretTrigger : MonoBehaviour {

    public BulletTurret bTurret;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            bTurret.ToggleFire();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player") {
            bTurret.ToggleFire();
        }
    }
}
