using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public GameObject Bullettest;
    public float fireDelay = 3000f;
    public float fireTime = 0f;
    public Transform firingPoint;
    public static bool bulletFired;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire(){
        if (fireTime < fireDelay) {
            fireTime += 1f;
        }
        else {
            Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y + 1.1f, firingPoint.position.z);
            GameObject firedBullet = Instantiate(Bullettest, enemyPos, Quaternion.identity);
            Vector3 direction = new Vector3(0, firingPoint.position.y, 0);
            firedBullet.GetComponent<Rigidbody>().velocity = direction * 2f;
            fireTime = 0f;
        }
    }
}
