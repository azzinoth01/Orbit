using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_handler : MonoBehaviour
{
    public GameObject pausePanel;

    private void Awake() {
        Globals.pauseHandler = this;
    }
    public void setPause() {
        Globals.pause = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void setResume() {
        Globals.pause = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }


    public void quitGame() {
        Application.Quit();
    }
}
