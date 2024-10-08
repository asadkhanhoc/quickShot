using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset between player and camera
    public float fixedYPosition = 0f; // Fixed Y position for the camera

    // Update is called once per frame
    void Update()
    {
        // Update camera's position to follow the player
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + offset.x, fixedYPosition + offset.y, offset.z);
        }
    }
}
