using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // Score variables
    public int score = 0; // Add score variable
    public TMP_Text scoreDisplay; // Reference to score text UI

    public int health;
    public TMP_Text healthDisplay;

    public GameObject winPanel;
    public TMP_Text winText;

    public GameObject losePanel;
    public TMP_Text loseText;

    Rigidbody2D rb;

    public GameObject enemySpawner; // Reference to the GameObject with the spawning script

    // Start is called before the first frame update
    void Start()
    {
 
        scoreDisplay.text = "Score: " + score; // Initialize score display
        rb = GetComponent<Rigidbody2D>();
        healthDisplay.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
                
        // check if player hits the portal
        if (collision.gameObject.tag == "Portal")
        {
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

    // Method to increment the score
    public void IncrementScore()
    {
        score++;
        UpdateScoreDisplay();
    }

    // Method to decrement the score
    public void DecrementScore()
    {
        score = Mathf.Max(0, score - 1); // Prevent negative score
        UpdateScoreDisplay();
    }

    // Update score display
    private void UpdateScoreDisplay()
    {
        if (scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + score;
        }
    }

}
