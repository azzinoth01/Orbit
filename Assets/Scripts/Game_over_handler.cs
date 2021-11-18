using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_over_handler : MonoBehaviour
{

    private void Awake() {
        Globals.gameoverHandler = this;
    }

    public void gameOver() {
        if (Globals.pause == true) {
            Debug.Log("pause stopped");
            Globals.pauseHandler.setResume();
        }


        // um die controll callbacks im InputSystem richtig zu stoppen
        Destroy(Globals.player);

        SceneManager.LoadScene(0);
    }
}
