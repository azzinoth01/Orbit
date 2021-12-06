using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject rebidningMenu;
    public void playGame() {

        SceneManager.LoadScene(1);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void onClickBackToMainMenu() {
        mainMenu.SetActive(true);
        rebidningMenu.SetActive(false);
    }

    public void onClickControls() {
        mainMenu.SetActive(false);
        rebidningMenu.SetActive(true);
    }
}
