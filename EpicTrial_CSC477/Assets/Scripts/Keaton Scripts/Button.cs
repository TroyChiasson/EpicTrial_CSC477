using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    private AudioManager am;
    public Material onLight;
    public GameObject buttonLight;
    private MeshRenderer renderer;
    public BulletTurret[] bulletTurrets = null;
    public LaserTurret[] laserTurrets = null;
    public Door[] doors = null;

    // Start is called before the first frame update
    void Start() {
        am = GameObject.Find("AM").GetComponent<AudioManager>();
        renderer = buttonLight.GetComponent<MeshRenderer>();
        Debug.Log(laserTurrets.Length);
    }

    public void TurnOn() {
        am.Play(3);
        for (int i = 0; i < bulletTurrets.Length; i++) { bulletTurrets[i].Deactivate(); }
        for (int i = 0; i < laserTurrets.Length; i++) { laserTurrets[i].Deactivate(); }
        for (int i = 0; i < doors.Length; i++) { doors[i].Open(); }
        renderer.material = onLight;
    }
}
