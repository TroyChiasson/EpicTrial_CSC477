using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurretTrigger : MonoBehaviour {

    public BulletTurret turret;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            turret.ToggleFire();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player") {
            turret.ToggleFire();
        }
    }
}
