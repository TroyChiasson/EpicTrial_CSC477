using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletTurret : MonoBehaviour {

    public Transform player;

    public AudioManager am;

    private bool activated = true;

    private float turnSpeed = 1f;

    private bool fireEnabled = false;
    private float fireSpeed = 1f;
    private float fireTime = 0;

    public Transform firingPoint;
    public GameObject BulletTest;

    public GameObject turretHead;

    private MeshRenderer renderer;
    public Material offMaterial;

    // Start is called before the first frame update
    void Start() {
        renderer = turretHead.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (!activated) { return; }
        Turn(DetermineAngle());
        CycleFiring();
    }

    public void Deactivate() {
        renderer.material = offMaterial;
        activated = false;
    }

    public void ToggleFire() { fireEnabled = !fireEnabled; }

    public void CycleFiring() {

        //do not do anything if firing isnt enabled
        if (!fireEnabled) { return; }

        //fire if firespeed has passed
        if (fireTime >= fireSpeed) {
            fireTime = 0;
            Fire();
        }

        //if firespeed has not passed, record current frame time
        else { fireTime += Time.deltaTime; }

    }

    private void Fire() {
        am.Play(0);
        Vector3 cannonEnd = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        GameObject firedBullet = Instantiate(BulletTest, cannonEnd, Quaternion.identity);
        Vector3 direction = player.position - firingPoint.position; // Shoot towards player
        firedBullet.GetComponent<Rigidbody>().velocity = direction.normalized * 20f; // Use normalized for consistent speed
    }

    /**determine the angle that the turret should rotate to to point towards the player**/
    private float DetermineAngle() {

        //trig to find angle between turret and player
        float xdif = player.position.x - turretHead.transform.position.x;
        float ydif = player.position.y - turretHead.transform.position.y;
        float radians = (float) Math.Atan2(xdif, ydif);
        float angle = radians * (float) (180 / Math.PI);

        //angle is offset by 90 degrees by default for some reason. correct that
        angle = angle - 90;

        //make negatives not negative
        if (angle < 0) { angle = 360 + angle; }

        //final angle
        return angle;
    }

    /**turn smoothly to a target angle**/
    private void Turn(float angle) {
        Quaternion targetAngle = Quaternion.Euler(0, 0, -angle);
        turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, targetAngle, Time.deltaTime * turnSpeed);
    }
}
