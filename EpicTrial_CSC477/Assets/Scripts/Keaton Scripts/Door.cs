using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private bool opening = false;
    private float openSpeed = 1f;
    private float openTime = 0f;
    private float maxWidth;

    void Start() {
        maxWidth = transform.localScale.x;
    }

    // Update is called once per frame
    void Update() {

        if (opening) {

            openTime += Time.deltaTime;

            if (openTime > openSpeed) {
                opening = false;
                transform.SetLocalScale(x: 0);
            }

            else {
                var curWidth = maxWidth * (openSpeed - openTime) / openSpeed;
                transform.SetLocalScale(curWidth);
            }
        }
    }

    public void Open() { opening = true; }
}
