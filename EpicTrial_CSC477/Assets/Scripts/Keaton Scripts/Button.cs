using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public Material onLight;
    private MeshRenderer renderer;
    public BulletTurret[] bts;

    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<MeshRenderer>();
    }

    public void TurnOn() {
        for(int i=0; i < bts.Length; i++) { bts[i].Deactivate(); }
        renderer.material = onLight;
    }
}
