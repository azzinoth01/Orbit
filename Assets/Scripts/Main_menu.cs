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
    public GameObject optionsMenu;
    public GameObject soundMenu;
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
    /// button um das Main menu wieder einzublenden vom Options Menu
    /// </summary>
    public void onClickBackToMainMenu() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    /// <summary>
    /// buttom um das Rebidning Menu einzublenden und das Options menu auszublenden
    /// </summary>
    public void onClickControls() {
        optionsMenu.SetActive(false);
        rebidningMenu.SetActive(true);
    }

    public void onClickOptions() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void onClickSound() {
        optionsMenu.SetActive(false);
        soundMenu.SetActive(true);
    }

    public void onClickBackToOptions() {
        rebidningMenu.SetActive(false);
        soundMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
