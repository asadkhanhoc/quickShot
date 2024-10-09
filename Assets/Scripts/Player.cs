using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using TMPro;

public class Player : MonoBehaviour
{
    //[SerializeField] private float bulletspeed = 10f;

    // Gun variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;

    public float jumpForce = 10f; // Jump force
    private bool isGrounded; // Check if the character is on the ground


    private Rigidbody2D rb;

    public GameObject enemySpawner; // Reference to the GameObject with the spawning script

    private float input;

    public float speed;
    private float fireTimer;


    public int health;
    public TMP_Text healthDisplay;

    public GameObject winPanel;
    public TMP_Text winText;

    public GameObject losePanel;
    public TMP_Text loseText;

    public AudioClip fireSound; // Assign the sound clip in the Inspector
    private AudioSource audioSource;

    // Variables for mobile button input
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
    private bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthDisplay.text = health.ToString();

        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 

        if (shoot && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        // Handle movement input for both keyboard and mobile buttons
        if (moveLeft)
        {
            input = -1;
        }
        else if (moveRight)
        {
            input = 1;
        }
        else
        {
            input = Input.GetAxisRaw("Horizontal");
        }

        // Flip character based on input direction
        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


    }

    private void Shoot()
    {
        // Play the fire sound
        PlayFireSound();

        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

        
    }

    void PlayFireSound()
    {
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }


    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(input * speed , rb.velocity.y);

        // Check for jump input
        if (jump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Set the vertical velocity
            isGrounded = false;
            jump = false; // Reset jump after performing it
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground check (simple, assumes ground or box has a tag "Ground")
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            isGrounded = true;
        }        

        // check if player hits the portal
        if (collision.gameObject.tag == "Portal")
        {
            winText.text = "Congratulations !!! You Passed \r\nLevel 1";
            winPanel.SetActive(true);
            levelEnd();
            StopEnemySpawning(); // Stop the enemy spawner
        }

    }

    private void levelEnd()
    {
            // Find all objects tagged as "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Villains");

            // Loop through each enemy and destroy them
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
    }

    // Function to stop enemy spawning
    void StopEnemySpawning()
    {
        if (enemySpawner != null)
        {
            Spawner spawner = enemySpawner.GetComponent<Spawner>();
            if (spawner != null)
            {
                spawner.StopSpawning(); // Custom function in the spawner script to stop spawning
            }
        }
    }


    public void TakeDamage(int damageAmount)
    {
        //source.Play();
        health -= damageAmount;
        healthDisplay.text = health.ToString();

        if (health <= 0)
        {
            health = 0;
            healthDisplay.text = health.ToString();
            Destroy(gameObject);
            loseText.text = "Oops !! You Are Dead..";
            losePanel.SetActive(true);
            levelEnd();
            StopEnemySpawning();
        }
    }

    // Functions for mobile button input
    public void OnMoveLeftButtonDown()
    {
        moveLeft = true;
    }

    public void OnMoveLeftButtonUp()
    {
        moveLeft = false;
    }

    public void OnMoveRightButtonDown()
    {
        moveRight = true;
    }

    public void OnMoveRightButtonUp()
    {
        moveRight = false;
    }

    public void OnJumpButtonDown()
    {
        jump = true;
    }

    public void OnShootButtonDown()
    {
        shoot = true;
    }

    public void OnShootButtonUp()
    {
        shoot = false;
    }
}
