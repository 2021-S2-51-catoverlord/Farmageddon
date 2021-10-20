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
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public GameObject craftingUI;
    public GameObject sellUI;
    public SaveManager saveManager;
    [Space]
    public Button SaveButton;
    public Button LoadButton;

    private void Awake()
    {
        if(saveManager == null)
        {
            saveManager = GetComponent<SaveManager>();
        }

        // Register button listeners (and assign actions to perform upon button press).
        SaveButton.onClick.AddListener(saveManager.SaveGame);
        LoadButton.onClick.AddListener(saveManager.LoadGame);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
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
        craftingUI.SetActive(false);
        sellUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SaveGame()
    {
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        Debug.Log("Previous Save Loaded");
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
#if UNITY_EDITOR
        // Quit the game in Editor mode.
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
