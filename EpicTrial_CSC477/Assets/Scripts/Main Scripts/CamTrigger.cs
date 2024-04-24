using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 newCamPos, newPlayerPos;

    CamController camControl; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MainPlayer")
        {
            camControl.minPos += newCamPos;
            camControl.maxPos += newCamPos;

            other.transform.position += newPlayerPos; 
        }
    }
}
