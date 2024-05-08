using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public Material onLight;
    public GameObject buttonLight;
    private MeshRenderer renderer;
    public BulletTurret[] bulletTurrets;
    public LaserTurret[] laserTurrets;
    public Door[] doors;

    // Start is called before the first frame update
    void Start() {
        renderer = buttonLight.GetComponent<MeshRenderer>();
    }

    public void TurnOn() {
        for (int i = 0; i < bulletTurrets.Length; i++) { bulletTurrets[i].Deactivate(); }
        for (int i = 0; i < laserTurrets.Length; i++) { laserTurrets[i].Deactivate(); Debug.Log("deactivated:"+i.ToString()); }
        for (int i = 0; i < doors.Length; i++) { doors[i].Open(); }
        renderer.material = onLight;
    }
}
