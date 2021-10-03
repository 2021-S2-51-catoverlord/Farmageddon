/*
 * This class contains the Main Menu,
 * which encapsulates the following methods:
 * 
 * Methods:
 * - PlayGame: Loads the game scene.
 * - EquitGame: Quits the game.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
