using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidMovement : MonoBehaviour
{
    public Transform[] positions; // Array of predefined stage positions
    public float speed = 1.0f; // Movement speed
    private int currentTarget = 0; // Current target position index
    public MeshRenderer meshRenderer; // Mesh Renderer component
    public Texture[] textures; // Textures for different stages

    public Transform kidTransform; // Reference to the Kid's transform
    public Transform endTransform; // Reference to the end bar transform
    public Slider Bar; // Slider to display the distance between Kid and Player

    private float maxDistance; // Maximum distance between Kid and Player
    public Transform KidBarPosition; // Transform to define the position of KidBar in world space

    void Start()
    {
        InitializeKidBar(); // Initialize the KidBar at start
    }

    void Update()
    {
        MoveToNextPosition();
        ChangeTextureBasedOnPosition();
        UpdateKidBarUI(); // Update the UI slider in each frame
    }

    void InitializeKidBar()
    {
        if (kidTransform != null && endTransform != null)
        {
            // Calculate the initial distance between Kid and Player as the max distance
            maxDistance = Vector3.Distance(kidTransform.position, endTransform.position);

            if (Bar != null)
            {
                Bar.maxValue = maxDistance;
                Bar.minValue = 0;
                Bar.value = maxDistance;
            }

            // Set KidBar to the specified position in world space
            if (KidBarPosition != null)
            {
                Bar.transform.position = KidBarPosition.position;
                Bar.transform.rotation = KidBarPosition.rotation;
            }
        }
    }

    void UpdateKidBarUI()
    {
        if (Bar != null && kidTransform != null && endTransform != null)
        {
            // Calculate the current distance between Kid and Player
            float currentDistance = Vector3.Distance(kidTransform.position, endTransform.position);

            // Update the slider value to reflect the current distance
            Bar.value = currentDistance;
        }
    }

    void MoveToNextPosition()
    {
        if (currentTarget >= positions.Length)
            return; // Stop moving when reaching the last position

        transform.position = Vector3.MoveTowards(transform.position, positions[currentTarget].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, positions[currentTarget].position) < 0.1f)
        {
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sandwich") && currentTarget > 0)
        {
            // Move back to the previous position if hit by a sandwich
            currentTarget--;
            UpdateKidBarUI(); // Update the UI slider after moving back
        }
    }

    void ChangeTextureBasedOnPosition()
    {
        if (currentTarget < textures.Length)
        {
            meshRenderer.material.mainTexture = textures[currentTarget];
        }
    }
}