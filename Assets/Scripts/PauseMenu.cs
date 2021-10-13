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
    public UIManager UIManager;
    [Space]
    public Button SaveButton;
    public Button LoadButton;

    private void Awake()
    {
        if(UIManager == null)
        {
            UIManager = GameObject.Find("CharacterUI").GetComponent<UIManager>();
        }

        // Register listeners (and assign actions to perform upon button press).
        SaveButton.onClick.AddListener(UIManager.SaveInventoryData);

        LoadButton.onClick.AddListener(UIManager.LoadInventoryData);
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
        inventoryUI.SetActive(inventoryUI.activeSelf);
        equipmentUI.SetActive(equipmentUI.activeSelf);
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
        // Tell save managers to save the game's current state.
        //SaveManager.SaveGame("savedGame.json");
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // Tell save managers to load the game.
        //SaveManager.LoadGame("savedGame.json");
        Debug.Log("Game Loaded");
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
