using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserTurret : MonoBehaviour {

    private Transform playerTrans;
    private MainPlayer player;

    private AudioManager am;

    private bool activated = true;

    private List<GameObject> destroyList = new List<GameObject>();

    public float turnSpeed = 1f;

    private bool fireEnabled = false;
    private float fireSpeed = 3f;
    private float fireTime = 0;
    private bool fireCharge = false;
    public float chargeSpeed = 3f;
    public float chargeTime = 0;
    private bool flickerEnabled = false;
    private bool flickerIsWhite = false;
    private float flickerSpeed = 0.05f;
    private float flickerTime = 0;
    private int flickerCount = 6;
    private int flickers = 0;
    private bool shoot = true;
    private float shootSpeed = 0.3f;
    private float shootTime = 0;

    public float laserMaxWidth;

    public Transform firingPoint;
    public GameObject BulletTest;

    public GameObject turretHead;
    public GameObject laser;
    private MeshRenderer laserRenderer;

    private MeshRenderer renderer;
    public Material offMaterial;

    private bool inLaser = false;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<MainPlayer>();
        playerTrans = player.transform;

        am = GameObject.Find("AM").GetComponent<AudioManager>();
        renderer = turretHead.GetComponent<MeshRenderer>();
        laserRenderer = laser.GetComponent<MeshRenderer>();
        laserRenderer.material.color = SetAlpha(laserRenderer.material.color, 0);
        laserMaxWidth = laser.transform.localScale.y;
    }

    public bool GetActive() { return activated; }

    // Update is called once per frame
    void Update() {
        if (!activated) { return; }
        if (!shoot && !flickerEnabled) { Turn(DetermineAngle()); }
        CycleFiring();

        //if the laser is active, destory everything inside of it (player, enemies, bullets)
        if (shoot) {
            if (inLaser) { player.Damage(3); }
            if (destroyList.Count > 0) {
                foreach (var obj in destroyList) { Destroy(obj); }
                destroyList = new List<GameObject>();
            }
        }
    }

    public void AddDestroyList(GameObject obj) {
        destroyList.Add(obj);
        Debug.Log("added to destroy list");
    }

    public void RemoveDestroyList(GameObject obj) {
        destroyList.Remove(obj);
    }

    public void Deactivate() {
        laserRenderer.material.color = SetAlpha(laserRenderer.material.color, 0);
        renderer.material = offMaterial;
        activated = shoot = flickerEnabled = fireCharge = false;
    }

    public void ToggleInLaser() { inLaser = !inLaser; }

    public void ToggleFire() { fireEnabled = !fireEnabled; }

    public void CycleFiring() {

        if (shoot) {

            if (shootTime >= shootSpeed) {
                shootTime = 0;
                shoot = false;
                laser.transform.SetLocalScale(y: 0);
            }

            else {
                shootTime += Time.deltaTime;
                var scale = laserMaxWidth * (shootSpeed - shootTime) / shootSpeed;
                laser.transform.SetLocalScale(y: scale);
            }
        }

        if (flickerEnabled) {

            if (flickerTime >= flickerSpeed) {
                flickerTime = 0;
                flickers++;
                if (flickerIsWhite) {
                    flickerIsWhite = false;
                    laserRenderer.material.color = Color.black;
                }
                else {
                    flickerIsWhite = true;
                    laserRenderer.material.color = Color.white;
                }
            }

            else { flickerTime += Time.deltaTime; }

            if (flickers >= flickerCount) {
                flickerEnabled = false;
                laserRenderer.material.color = Color.red;
                shoot = true;
            }

            return;
        }

        if (fireCharge) {

            if (chargeTime >= chargeSpeed) {
                chargeTime = 0;
                fireCharge = false;
                laserRenderer.material.color = Color.white;
                flickerEnabled = true;
                flickerIsWhite = true;
                flickers = 0;
                laser.transform.SetLocalScale(y: laserMaxWidth);
                am.Play(1);
            }

            else {
                var alpha = chargeTime / chargeSpeed / 3;
                var scale = laserMaxWidth * chargeTime / chargeSpeed;
                laserRenderer.material.color = SetAlpha(laserRenderer.material.color, alpha);
                laser.transform.SetLocalScale(y: scale);
                chargeTime += Time.deltaTime;
            }

            return;
        }

        //do not do anything if firing isnt enabled
        if (!fireEnabled) { return; }

        //fire if firespeed has passed
        if (fireTime >= fireSpeed) {
            fireTime = 0;
            fireCharge = true;
            am.Play(2);
            laserRenderer.material.color = Color.cyan;
            laserRenderer.material.color = SetAlpha(laserRenderer.material.color, 0);
        }

        //if firespeed has not passed, record current frame time
        else { fireTime += Time.deltaTime; }

    }

    /**determine the angle that the turret should rotate to to point towards the player**/
    private float DetermineAngle() {

        //trig to find angle between turret and player
        float xdif = playerTrans.position.x - turretHead.transform.position.x;
        float ydif = playerTrans.position.y - turretHead.transform.position.y;
        float radians = (float)Math.Atan2(xdif, ydif);
        float angle = radians * (float)(180 / Math.PI);

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

    private Color SetAlpha(Color color, float alpha) {
        color.a = alpha;
        return color;
    }
}
