using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public Material onLight;
    private MeshRenderer renderer;
    public BulletTurret[] bts;
    public Door[] ds;

    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<MeshRenderer>();
    }

    public void TurnOn() {
        for (int i=0; i < bts.Length; i++) { bts[i].Deactivate(); }
        for (int i=0; i < ds.Length; i++) { ds[i].Open(); }
        renderer.material = onLight;
    }
}
