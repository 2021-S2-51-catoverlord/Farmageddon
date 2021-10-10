using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public UIManager uiManager;
    [Space]
    public Button SaveButton;
    public Button LoadButton;

    private void Awake()
    {
        if(uiManager == null)
        {
            uiManager = GameObject.Find("CharacterUI").GetComponent<UIManager>();
        }

        //SaveButton.onClick.AddListener(SaveGame);
        SaveButton.onClick.AddListener(uiManager.SaveInventoryData);
        //LoadButton.onClick.AddListener(LoadGame);
        LoadButton.onClick.AddListener(uiManager.LoadInventoryData);
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
        //UIManager.SaveInventoryData();
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // Tell save managers to load the game.
        //SaveManager.LoadGame("savedGame.json");
        //UIManager.LoadInventoryData();
        Debug.Log("Game Loaded");
        Resume();
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
