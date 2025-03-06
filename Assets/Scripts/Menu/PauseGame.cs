using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // El Panel con los botones
    public Button menuButton; // Botón que representa el PNG
    private bool isPaused = false;

    void Awake()
    {
        ResumeGame();
    }

    void Start()
    {
        
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(PauseGame);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
       
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
