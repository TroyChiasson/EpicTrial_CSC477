using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;

    private float playerSpeed = 1.0f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        //movement keys being pressed
        if (Game.Instance.input.Default.Up.WasPressedThisFrame()) { up = true; }
        if (Game.Instance.input.Default.Down.WasPressedThisFrame()) { down = true; }
        if (Game.Instance.input.Default.Left.WasPressedThisFrame()) { left = true; }
        if (Game.Instance.input.Default.Right.WasPressedThisFrame()) { right = true; }

        //movement keys being released
        if (Game.Instance.input.Default.Up.WasReleasedThisFrame()) { up = false; }
        if (Game.Instance.input.Default.Down.WasReleasedThisFrame()) { down = false; }
        if (Game.Instance.input.Default.Left.WasReleasedThisFrame()) { left = false; }
        if (Game.Instance.input.Default.Right.WasReleasedThisFrame()) { right = false; }

        if (up) {
            var vel = rb.velocity;
            vel.y = playerSpeed;
            rb.velocity = vel;
        }
        else if (down) {
            var vel = rb.velocity;
            vel.y = -playerSpeed;
            rb.velocity = vel;
        }
        else {
            var vel = rb.velocity;
            vel.y = 0;
            rb.velocity = vel;
        }

        if (left) {
            var vel = rb.velocity;
            vel.x = -playerSpeed;
            rb.velocity = vel;
        }
        else if (right) {
            var vel = rb.velocity;
            vel.x = playerSpeed;
            rb.velocity = vel;
        }
        else {
            var vel = rb.velocity;
            vel.x = 0;
            rb.velocity = vel;
        }

    }
}
