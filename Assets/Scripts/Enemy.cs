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

    
   

    // Start is called before the first frame update
    void Start()
    {
       
       

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Set an initial direction, e.g., moving right
        direction = Vector2.left;

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
            ScoreManager.Instance.DecrementScore(); // Decrement score when player hits
            playerScript.TakeDamage(damage);
            //Instantiate(transform.position, Quaternion.identity);
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
            ScoreManager.Instance.IncrementScore(); // Increment score when bullet hits
            Destroy(hitObject.gameObject); // Destroy the bullet
            Destroy(gameObject); // Destroy the villain
        }

        // Check if the object that triggered the event is tagged as a "Bullet"
        if (hitObject.CompareTag("Box"))
        {
            // Reverse the direction
            direction = -direction; // Change direction

        }

    }
}
