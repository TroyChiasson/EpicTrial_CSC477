using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public GameObject Bullettest;
    public float fireDelay = 0f;
    public float fireTime = 3000f;
    public Transform firingPoint;
    public Transform targetPlayer;
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
        if (fireTime > fireDelay) {
            fireDelay += 1f;
        }
        else {
            Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
            GameObject firedBullet = Instantiate(Bullettest, enemyPos, Quaternion.identity);
            Vector3 direction = enemyPos + transform.forward;
            firedBullet.GetComponent<Rigidbody>().velocity = direction * 2f;
            fireDelay = 0f;
        }
    }
}
