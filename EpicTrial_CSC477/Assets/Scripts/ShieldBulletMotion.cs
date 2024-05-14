using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBulletMotion : MonoBehaviour
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
    //We want the shield bullet to affect enemies and bosses
    void OnCollisionEnter(Collision other) 
    {
        Destroy(this.gameObject);
        if (other.gameObject.tag == "Enemy") 
        {
        scoreManager.instance.AddPoints(200);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        }

    }
   
}
