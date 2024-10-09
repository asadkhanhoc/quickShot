using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        // Check if the object that triggered the event is tagged as a "Bullet"
        if (hitObject.CompareTag("Bullet"))
        {
            Destroy(hitObject.gameObject); // Destroy the bullet
          
        }
    }
}
