using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX;

    [SerializeField]
    float moveSpeed = 5f, jumpForce = 800f;

   
    Vector3 localScale;

    // Gun variables
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;

    public AudioClip fireSound; // Assign the sound clip in the Inspector
    private AudioSource audioSource;

    

    // Start is called before the first frame update
    void Start()
    {
        
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        

        dirX = CrossPlatformInputManager.GetAxis("Horizontal");

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1") && fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void CheckWhereToFace()
    {
        if(dirX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dirX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Jump()
    {
        if (rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Fire()
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
}
