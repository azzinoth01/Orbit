using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{

    public void playGame() {

        SceneManager.LoadScene(1);
    }

    public void quitGame() {
        Application.Quit();
    }
}
