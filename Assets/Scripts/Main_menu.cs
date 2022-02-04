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
    public GameObject credits;

    /// <summary>
    /// load the first game Scene
    /// </summary>
    public void playGame() {

        PlayerSave s = PlayerSave.loadSettings();

        if (s == null) {
            s = new PlayerSave();
        }

        if (s.TutorialPlayed == true) {
            SceneManager.LoadScene(1);
        }
        else {
            // lade tutorial Scene
            SceneManager.LoadScene(4);
        }


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

    //Benni hat hier sound und Rebind hinzugefügt
    public void onClickBackToMainMenu() {
        mainMenu.SetActive(true);

        if (optionsMenu != null) {
            optionsMenu.SetActive(false);
        }
        if (soundMenu != null) {
            soundMenu.SetActive(false);
        }
        if (rebidningMenu != null) {
            rebidningMenu.SetActive(false);
        }
        if (credits != null) {
            credits.SetActive(false);
        }

    }

    /// <summary>
    /// buttom um das Rebidning Menu einzublenden und das Options menu auszublenden
    /// </summary>

    //Bennie: geändert Options to Main
    public void onClickControls() {
        mainMenu.SetActive(false);
        rebidningMenu.SetActive(true);
    }

    public void onClickOptions() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    //Bennie: geändert Options to Main
    public void onClickSound() {
        mainMenu.SetActive(false);
        soundMenu.SetActive(true);
    }
    //momentan nicht in Benutzung
    public void onClickBackToOptions() {
        rebidningMenu.SetActive(false);
        soundMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void onClickOptions(int index) {
        Globals.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(index);
    }

    public void onClickCredits() {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }
}
