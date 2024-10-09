using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Trigger function when the player reaches the door
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with the door is the player
        if (collision.CompareTag("Player"))
        {
            DestroyAllEnemies(); // Call function to destroy all enemies
        }
    }

    // Function to destroy all enemies
    void DestroyAllEnemies()
    {
        // Find all objects tagged as "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Villains");

        // Loop through each enemy and destroy them
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
