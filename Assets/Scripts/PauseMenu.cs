/*
 * This class contains the Pause Menu,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - Update method.
 * - Resume: Resumes the game.
 * - PauseGame: Pauses game when the pause menu is active.
 * - SaveGame: Saves the game.
 * - LoadGame: Loads the game.
 * - QuitGame: Quits game.
 */

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
        Debug.Log("Game Save");
    }

    public void LoadGame()
    {
        Debug.Log("Game Loaded");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
