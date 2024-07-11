using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the Pause Menu UI
    private bool isPaused = false;
    private GameInputActions inputActions;

    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;

    [Header("Pause Menu Buttons")]
    public Button resumeButton;
    public Button mainMenuButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;
   
    public List<Button> returnButtons;

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

        // Hook events
        resumeButton.onClick.AddListener(Resume);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnablePauseMenu);
        }

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

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void EnablePauseMenu()
    {
        pauseMenuUI.SetActive(true);
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }
    public void EnableMainMenu()
    {
        pauseMenuUI.SetActive(false);
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }
    public void EnableOption()
    {
        pauseMenuUI.SetActive(false);
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
    }

    public void EnableAbout()
    {
        pauseMenuUI.SetActive(false);
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
    }

}
