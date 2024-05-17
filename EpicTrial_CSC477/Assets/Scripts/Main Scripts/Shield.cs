using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Assign the player object in the inspector
    [SerializeField] private float orbitDistance = 1f; // Set the desired distance from the player
    [SerializeField] private float rotationSpeed = 100f; // Adjust the rotation speed

    private AudioManager am;

    public GameObject Bullettest;
    public Transform firingPoint;
    public static bool bulletFired;

    // Shield Health Stuff
    public List<GameObject> shieldSegments;
    public GameObject playerShield;
    public GameObject player;
    public int shieldHealth;
    private int maxShieldHealth = 6;
    private Coroutine resetShieldCoroutine;
    private bool isInvulnerable = false;
    private bool isFlashing = false;

    void Start()
    {
        am = GameObject.Find("AM").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        shieldHealth = 6;
    }

    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.transform.position;
        direction.z = 0f; // Ensure rotation happens in 2D plane

        // Calculate desired rotation based on player and mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object around the player at a set distance (with Z at 0)
        Vector3 desiredPosition = player.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * orbitDistance;
        desiredPosition.z = 0f; // Force Z to 0
        transform.position = desiredPosition;

        // Apply rotation smoothly
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

        if ((isInvulnerable && shieldHealth <= 0) || (resetShieldCoroutine != null))
        {
            ToggleShieldFlashing();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet" && !isInvulnerable)
        {
            Reflect();
        }

        if (other.gameObject.tag == "Bomber" && !isInvulnerable)
        {
            player.GetComponent<MainPlayer>().Damage(1);
        }

        if (shieldHealth <= 0)
        {
            SetShieldActive(false);

            // Start the coroutine to reset the shield
            if (resetShieldCoroutine == null)
            {
                UpdateShield();
                resetShieldCoroutine = StartCoroutine(ResetShieldCoroutine());
            }
        }
    }

    void Reflect()
    {
        am.Play(4);
        print("howdy");
        Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        GameObject firedBullet = Instantiate(Bullettest, enemyPos, Quaternion.identity);
        Vector3 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.transform.position;
        firedBullet.GetComponent<Rigidbody>().velocity = direction * 2f;
        shieldHealth--;
        UpdateShield();
        isInvulnerable = true; 
        StartCoroutine(ResetInvulnerability());
    }

    IEnumerator ResetInvulnerability()
    {
        yield return new WaitForSeconds(1f); 
        isInvulnerable = false; 
    }

    void UpdateShield()
    {
        // Loop through the shieldSegments list
        for (int i = 0; i < shieldSegments.Count; i++)
        {
            // Set the GameObject active based on shieldHealth
            shieldSegments[i].SetActive(i < shieldHealth);
        }
    }

    // Coroutine to reset the shield after a delay
    IEnumerator ResetShieldCoroutine()
    {
        yield return new WaitForSeconds(5f); 

        shieldHealth = maxShieldHealth;
        UpdateShield();
        SetShieldActive(true);

        resetShieldCoroutine = null;
    }

    // Function to toggle sprite renderer and collider
    void SetShieldActive(bool active)
    {
        if (!active) { am.Play(5); }
        playerShield.GetComponent<SpriteRenderer>().enabled = active;
        playerShield.GetComponent<Collider>().enabled = active;
    }

    void ToggleShieldFlashing()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashShield());
        }
    }

    IEnumerator FlashShield()
    {
        while ((isInvulnerable && shieldHealth <= 0) || (resetShieldCoroutine != null))
        {
            yield return new WaitForSeconds(0.5f);
            foreach (var segment in shieldSegments)
            {
                segment.SetActive(!segment.activeSelf);
            }
        }
        isFlashing = false;
        UpdateShield();
    }
}
