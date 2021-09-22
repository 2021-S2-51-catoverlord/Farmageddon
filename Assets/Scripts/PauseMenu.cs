using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject equipmentUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        inventoryUI.SetActive(false);
        equipmentUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SaveGame()
    {
        //string fileName = "saved_" + DateTime.Now.ToString("s") + ".json"; 
        // Tell  save manager to save the game's current state.
        SaveManager.SaveGame("savedGame.json");
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // Tell save manager to load the game.
        SaveManager.LoadGame("savedGame.json");
        Debug.Log("Game Loaded");

        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
