using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidMovement : MonoBehaviour
{
    public Transform[] positions; // Array of predefined stage positions
    public float speed = 1.0f; // Movement speed
    private int currentTarget = 0; // Current target position index
    public MeshRenderer meshRenderer; // Mesh Renderer component
    public Texture[] textures; // Textures for different stages

    [Header("sandwich make kid move backward")]
    private int previousTarget = -1; // To store the index of the previous position


    void Update()
    {
        MoveToNextPosition();
        ChangeTextureBasedOnPosition();
    }

    void MoveToNextPosition()
    {
        if (currentTarget >= positions.Length)
        {
            return; // Stop moving when reaching the last position
        }

        transform.position = Vector3.MoveTowards(transform.position, positions[currentTarget].position, speed * Time.deltaTime);

        // Check if the current target position is reached
        if (Vector3.Distance(transform.position, positions[currentTarget].position) < 0.1f)
        {
            previousTarget = currentTarget; // Store current target as previous
            if (currentTarget == positions.Length - 1)
            {
                // end game logic
            }
            else
            {
                currentTarget++; // Move to the next position
            }
        }
    }

    //sandwich make kid move backward
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sandwich") && previousTarget != -1)
        {
            // Move back to the previous position if hit by a sandwich
            currentTarget = previousTarget;
            previousTarget = Mathf.Max(0, currentTarget - 1); // Update previous to the one before, if possible
        }
    }

    void ChangeTextureBasedOnPosition()
    {
        // Change texture based on the current target position index
        if (currentTarget < textures.Length)
        {
            meshRenderer.material.mainTexture = textures[currentTarget];
        }
    }
}