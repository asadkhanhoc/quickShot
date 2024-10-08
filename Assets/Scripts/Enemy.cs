using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    //private Rigidbody2D rb;

    //public Vector3 fixedHeight; // The Y position villains should stay at
   

    Player playerScript;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
       
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        // Keep Y position fixed at ground level or above
        //if (transform.position.y < fixedHeight.y)
        //{
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        //}
    }

    void OnTriggerEnter2D(Collider2D hitObject)
    {

        if (hitObject.tag == "Player")
        {
            //playerScript.TakeDamage(damage);
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
            Destroy(hitObject.gameObject); // Destroy the bullet
            Destroy(gameObject); // Destroy the villain
        }

    }

  


}
