using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

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

    public GameObject losePanel;

    private Rigidbody2D rb;
   

    private float input;

    public float speed;
    private float fireTimer;

    private Vector2 mousePos;

    public int health;
    public Text healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }


        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed , rb.velocity.y);

        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Set the vertical velocity
            isGrounded = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground check (simple, assumes ground has a tag "Ground")
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        //source.Play();
        health -= damageAmount;
        healthDisplay.text = health.ToString();

        if (health <= 0)
        {
            losePanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
