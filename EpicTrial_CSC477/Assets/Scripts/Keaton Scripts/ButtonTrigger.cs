using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    public Button button;
    private bool activated = false;

    private void OnTriggerEnter(Collider other) {
        if (activated) { return; }
        if (other.gameObject.tag == "ShieldBullet") {
            button.TurnOn();
            activated = true;
        }
    }
}
