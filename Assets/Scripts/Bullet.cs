using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    //public GameObject box;
  
   

    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;


    private void Start()
    {
 

        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);

       // box = GameObject.FindGameObjectWithTag("Box").GetComponent<GameObject>();

    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

}
