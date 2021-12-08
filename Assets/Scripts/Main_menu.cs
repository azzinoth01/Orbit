using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// classe um das main menu zu kontrolieren
/// </summary>
public class Main_menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject rebidningMenu;

    /// <summary>
    /// load the first game Scene
    /// </summary>
    public void playGame() {

        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// quit the game
    /// </summary>
    public void quitGame() {
        Application.Quit();
    }

    /// <summary>
    /// button um das Main menu wieder einzublenden vom Rebinding Menu
    /// </summary>
    public void onClickBackToMainMenu() {
        mainMenu.SetActive(true);
        rebidningMenu.SetActive(false);
    }

    /// <summary>
    /// buttom um das Rebidning Menu einzublenden und das Main menu auszublenden
    /// </summary>
    public void onClickControls() {
        mainMenu.SetActive(false);
        rebidningMenu.SetActive(true);
    }
}
