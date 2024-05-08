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
    public static float playerSpeed = 5.0f;

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
    public static int playerHealth;
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
        UpdateHealthUI();
    }

    public void Damage(int dmg) {
        playerHealth -= dmg;
        UpdateHealthUI();

        // Check if player health has reached zero
        if (playerHealth <= 0) { SceneManager.LoadScene("MainDeath"); }
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
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the desired tag
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Explosion")) //Add explosion to the collision
        {
            // Reduce player health
            Damage(1);
        }
    }

    public void UpdateHealthUI()
    { 
        heart1.SetActive(playerHealth >= 1);
        heart2.SetActive(playerHealth >= 2);
        heart3.SetActive(playerHealth >= 3);
    }
}
