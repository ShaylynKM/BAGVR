using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KidMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions; // Array of predefined stage positions

    //public float speed = 1.0f; // Movement speed

    [SerializeField]
    private float[] speeds;

    [SerializeField]
    private float[] MusicPitches;
    public string KidbackgroundMusicName = "test";

    [SerializeField]
    private float currentSpeed;

    private int currentTarget = 0; // Current target position index

    [SerializeField]
    private MeshRenderer meshRenderer; // Mesh Renderer component

    [SerializeField]
    private Texture[] textures; // Textures for different stages

    [SerializeField]
    private Transform kidTransform; // Reference to the Kid's transform

    [SerializeField]
    private Transform endTransform; // Reference to the end bar transform

    [SerializeField]
    private Slider Bar; // Slider to display the distance between Kid and Player

    private float maxDistance; // Maximum distance between Kid and Player

    [SerializeField]
    private Transform KidBarPosition; // Transform to define the position of KidBar in world space

    [SerializeField]
    private ParticleSystem crumbsParticle;

    private PauseMenu _pauseMenu;

    void Start()
    {
        _pauseMenu = FindAnyObjectByType<PauseMenu>();
        //AudioManager.instance.Play("test");
        InitializeKidBar(); // Initialize the KidBar at start
    }

    void Update()
    {
        MoveToNextPosition();
        ChangeTextureBasedOnPosition();
        ChangeSpeedBasedOnPosition();
        ChangePitchBasedOnPosition();
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
        if (_pauseMenu.isPaused)
            return;
        if (currentTarget >= positions.Length)
            return; // Stop moving when reaching the last position

        transform.position = Vector3.MoveTowards(transform.position, positions[currentTarget].position, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, positions[currentTarget].position) < 0.1f)
        {
            if (currentTarget == positions.Length - 1)
            {
                // end game logic
                SceneManager.LoadScene("MainMenu");
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
            ParticleSystem p = Instantiate(crumbsParticle, null);
            p.transform.position = transform.position;
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

    // Changes how fast the kid is based on the position in regards to the list of positions
    void ChangeSpeedBasedOnPosition()
    {
        if(currentTarget < speeds.Length)
        {
            currentSpeed = speeds[currentTarget];
        }
    }
    void ChangePitchBasedOnPosition()
    {
        if (currentTarget < MusicPitches.Length)
        {
            float currentPitch = MusicPitches[currentTarget];
            AudioManager.instance.SetMusicPitch(KidbackgroundMusicName, currentPitch);
        }
    }
}
