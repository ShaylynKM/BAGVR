using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    private bool isPaused = false;
    private GameInputActions inputActions;

    void Awake()
    {
        inputActions = new GameInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.UI.Pause.performed += OnPausePerformed;
    }

    void OnDisable()
    {
        inputActions.UI.Pause.performed -= OnPausePerformed;
        inputActions.Disable();
    }

    void Start()
    {
        // Ensure the pause menu is hidden at the start
        pauseMenuUI.SetActive(false);
    }

    // Keyboard input for testing
    void Update()
    {
       
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        // Hide the pause menu and resume the game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    public void Pause()
    {
        // Show the pause menu and pause the game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
    }

    public void QuitGame()
    {
        // Handle quit game logic (e.g., load main menu or quit application)
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
