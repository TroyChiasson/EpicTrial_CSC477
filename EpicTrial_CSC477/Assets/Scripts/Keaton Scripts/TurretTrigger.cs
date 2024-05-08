using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour {

    public BulletTurret bTurret;
    public LaserTurret lTurret;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            if (bTurret != null) { bTurret.ToggleFire(); }
            if (lTurret != null) { lTurret.ToggleFire(); }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Player") {
            if (bTurret != null) { bTurret.ToggleFire(); }
            if (lTurret != null) { lTurret.ToggleFire(); }
        }
    }
}
