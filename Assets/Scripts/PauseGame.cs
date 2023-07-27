using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuButtons;
    public GameObject pauseMenuQuitRoutine;
    public GameObject pauseUIWindows;
    public AudioSource levelMusic;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                GamePause();
            }
        }
    }

    public void Resume()
    {
        if(levelMusic)
            levelMusic.Play();
        pauseMenuUI.SetActive(false);
        pauseMenuQuitRoutine.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    private void GamePause()
    {
        if (levelMusic)
            levelMusic.Pause();
        pauseMenuUI.SetActive(true);
        pauseMenuButtons.SetActive(true);
        pauseUIWindows.SetActive(true);
        pauseMenuQuitRoutine.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        print("Load Menu");
    }

    public void QuitGame()
    {
        print("Quiting Game");
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

}
