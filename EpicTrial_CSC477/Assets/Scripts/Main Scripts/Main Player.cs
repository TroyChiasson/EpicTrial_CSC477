using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Rigidbody rb;

    //dash stuffs
    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    // Player Health 
    public int playerHealth;
    private int startHealth = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    //start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        activeMoveSpeed = playerSpeed;
        playerHealth = startHealth; 
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

        // Diagonal movement
        if (up && left) rb.velocity = new Vector3(activeMoveSpeed * DIAGMULT, activeMoveSpeed * DIAGMULT, 0);
        else if (up && right) rb.velocity = new Vector3(-activeMoveSpeed * DIAGMULT, activeMoveSpeed * DIAGMULT, 0);
        else if (down && left) rb.velocity = new Vector3(activeMoveSpeed * DIAGMULT, -activeMoveSpeed * DIAGMULT, 0);
        else if (down && right) rb.velocity = new Vector3(-activeMoveSpeed * DIAGMULT, -activeMoveSpeed * DIAGMULT, 0);

        else {
            // Straight x or y movement
                // Y movement
                if (up) rb.velocity = new Vector3(rb.velocity.x, activeMoveSpeed, 0);
                else if (down) rb.velocity = new Vector3(rb.velocity.x, -activeMoveSpeed, 0);
                else rb.velocity = new Vector3(rb.velocity.x, 0, 0);

                // X movement
                if (left) rb.velocity = new Vector3(activeMoveSpeed, rb.velocity.y, 0);
                else if (right) rb.velocity = new Vector3(-activeMoveSpeed, -rb.velocity.y, 0);
                else rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }


        //dash stuffs

        if (Game.Instance.input.Default.Space.WasPressedThisFrame())
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = playerSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        // Health Check 

        /*
        if (damage)
        {
            CheckHealth();
        }
        */
    }

    void CheckHealth()
    {
        if (playerHealth == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (playerHealth == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if (playerHealth == 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            SceneManager.LoadScene("MainDeath");
        }
    }
}
