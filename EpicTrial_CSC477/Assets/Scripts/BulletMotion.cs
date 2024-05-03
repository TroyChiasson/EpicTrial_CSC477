using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision other) {
            Destroy(this.gameObject);
            if (other.gameObject.tag == "Enemy") {
                Destroy(other.gameObject);
            }
    }
   
}
