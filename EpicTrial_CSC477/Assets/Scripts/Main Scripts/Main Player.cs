using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour {

    //WASD key pressed
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;

    //save old WASD presses (cannot move while dashing, remember what they were before dashing)
    private bool oldUp = false;
    private bool oldDown = false;
    private bool oldLeft = false;
    private bool oldRight = false;

    //how fast the player is currently moving
    private float activeMoveSpeed;

    //how fast the player moves in x or y direction normally
    public static float playerSpeed = 5.0f;

    //when moving diagonally, multiply x and y speeds by this amount
    private const float DIAGMULT = 0.75f;

    //player physics
    private Rigidbody rb;

    //how fast the player moves mid-dash
    public float dashSpeed = 15f;

    //length of dash and cooldown
    public float dashLength = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

    //player health 
    public static int playerHealth;
    private int startHealth = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    //start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        activeMoveSpeed = playerSpeed;
        playerHealth = startHealth;
        UpdateHealthUI();
    }

    /**reduce player health by dmg**/
    public void Damage(int dmg) {

        //reduce health and update UI
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

        //direction the player will move this frame
        bool nowUp; bool nowDown; bool nowLeft; bool nowRight;

        //if dash counter is active, we move according to old movement values
        if (dashCounter > 0) {
            nowUp = oldUp;
            nowDown = oldDown;
            nowLeft = oldLeft;
            nowRight = oldRight;
        }

        //if dash counter is not active, move according to current movement values
        else {
            nowUp = up;
            nowDown = down;
            nowLeft = left;
            nowRight = right;
        }

        //cannot move in opposite directions at once
        if (nowUp && nowDown) { nowUp = nowDown = false; }
        if (nowLeft && nowRight) { nowLeft = nowRight = false; }

        //diagonal movement
        if (nowUp && nowLeft) rb.velocity = new Vector3(activeMoveSpeed * DIAGMULT, activeMoveSpeed * DIAGMULT, 0);
        else if (nowUp && nowRight) rb.velocity = new Vector3(-activeMoveSpeed * DIAGMULT, activeMoveSpeed * DIAGMULT, 0);
        else if (nowDown && nowLeft) rb.velocity = new Vector3(activeMoveSpeed * DIAGMULT, -activeMoveSpeed * DIAGMULT, 0);
        else if (nowDown && nowRight) rb.velocity = new Vector3(-activeMoveSpeed * DIAGMULT, -activeMoveSpeed * DIAGMULT, 0);

        //straight x or y movement
        else {

            //y
            if (nowUp) rb.velocity = new Vector3(rb.velocity.x, activeMoveSpeed, 0);
            else if (nowDown) rb.velocity = new Vector3(rb.velocity.x, -activeMoveSpeed, 0);
            else rb.velocity = new Vector3(rb.velocity.x, 0, 0);

            //x
            if (nowLeft) rb.velocity = new Vector3(activeMoveSpeed, rb.velocity.y, 0);
            else if (nowRight) rb.velocity = new Vector3(-activeMoveSpeed, -rb.velocity.y, 0);
            else rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        //if spacebar was pressed, begin dash
        if (Game.Instance.input.Default.Space.WasPressedThisFrame()) {

            //ensure we arent already currently dashing or waiting for dash cool down
            if (dashCoolCounter <= 0 && dashCounter <= 0) {

                //begin dash
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                //remember directions before dash
                oldUp = up;
                oldDown = down;
                oldLeft = left;
                oldRight = right;
            }
        }

        //mid-dash
        if(dashCounter > 0) {

            //decrement dash counter as time goes by
            dashCounter -= Time.deltaTime;

            //stop dash if dash counter is finished
            if (dashCounter <= 0) {
                activeMoveSpeed = playerSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        //decrement dash cool down counter
        if(dashCoolCounter > 0) { dashCoolCounter -= Time.deltaTime; }
    }

    /**player collided with another object**/
    void OnCollisionEnter(Collision collision)
    {
        //objects, bullets, and explosions deal 1 damage
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Explosion")) {
            Damage(1);
        }
    }

    /**update UI according to health**/
    public void UpdateHealthUI() { 
        heart1.SetActive(playerHealth >= 1);
        heart2.SetActive(playerHealth >= 2);
        heart3.SetActive(playerHealth >= 3);
    }
}
