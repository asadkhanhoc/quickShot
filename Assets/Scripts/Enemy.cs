using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Ensure this is included

public class Enemy : MonoBehaviour
{
    public float speed;

    private Vector2 direction; // The current movement direction

    Player playerScript;

    public int damage;


    private int health; // Health variable to track shots taken


    // Start is called before the first frame update
    void Start()
    {
 
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Set an initial direction, e.g., moving right
        direction = Vector2.left;

        // Set health based on the current level
        SetHealthBasedOnLevel();
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(direction * speed * Time.deltaTime);   
    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {

        if (hitObject.tag == "Player")
        {
            playerScript.DecrementScore(); // Decrement score when player hits
            playerScript.TakeDamage(damage);
            
            Destroy(gameObject);
           
        }

        if (hitObject.tag == "Wall")
        {
            //playerScript.TakeDamage(damage);
            //Instantiate(transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Check if the object that triggered the event is tagged as a "Bullet"
        if (hitObject.CompareTag("Bullet"))
        {
            // Decrement health when hit by a bullet
            TakeDamageEnemy();

            playerScript.IncrementScore(); // Increment score when bullet hits
            Destroy(hitObject.gameObject); // Destroy the bullet
        }

        // Check if the object that triggered the event is tagged as a "Bullet"
        if (hitObject.CompareTag("Box"))
        {
            // Reverse the direction
            direction = -direction; // Change direction

        }

    }

    private void SetHealthBasedOnLevel()
    {
        int currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex; // Assuming levels are in build index order
        //Debug.Log("Current Level: " + currentLevel); // Log the current level index

        switch (currentLevel)
        {
            case 1:
                health = 1;
                break;

            case 2: health = 3;
                break;

            case 3: health = 5;
                break;
        }

        //if (currentLevel == 1) // Level 1
        //{
        //    health = 1; // Default, enemy dies after 1 shot
        //}
        //else if (currentLevel == 2) // Level 2
        //{
        //    health = 3; // Enemy dies after 3 shots
        //}
        //else if (currentLevel == 3) // Level 3
        //{
        //    health = 5; // Enemy dies after 5 shots
        //}
    }

    private void TakeDamageEnemy()
    {
        health--; // Decrement health
        Debug.Log("Enemy hit! Current health: " + health); // Log current health after damage
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy enemy when health is 0
        }
    }
}
