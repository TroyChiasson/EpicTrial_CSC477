using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{

    //WASD key pressed
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;

    //how fast the player moves in x or y direction
    public float playerSpeed = 5.0f;

    //when moving diagonally, multiply x and y speeds by this amount
    private const float DIAGMULT = 0.75f;

    //player physics
    private Rigidbody2D rb;

    //start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //update is called once per frame
    void Update()
    {

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

        //diagonal movement
        if (up && left) { rb.SetVel(x: -playerSpeed * DIAGMULT, y: playerSpeed * DIAGMULT); }
        else if (up && right) { rb.SetVel(x: playerSpeed * DIAGMULT, y: playerSpeed * DIAGMULT); }
        else if (down && left) { rb.SetVel(x: -playerSpeed * DIAGMULT, y: -playerSpeed * DIAGMULT); }
        else if (down && right) { rb.SetVel(x: playerSpeed * DIAGMULT, y: -playerSpeed * DIAGMULT); }

        //straight x or y movement
        else
        {

            //y movement
            if (up) { rb.SetVel(y: playerSpeed); }
            else if (down) { rb.SetVel(y: -playerSpeed); }
            else { rb.SetVel(y: 0); }

            //x movement
            if (left) { rb.SetVel(x: -playerSpeed); }
            else if (right) { rb.SetVel(x: playerSpeed); }
            else { rb.SetVel(x: 0); }
        }

    }
}
