using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMotion : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab of the explosion effect

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //We only want the enemy bullet to affect the player, and destroy itself... damage is handled in player script
    void OnCollisionEnter(Collision other) 
    {
            Destroy(this.gameObject);
    }
     
}
