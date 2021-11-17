using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_handler : MonoBehaviour
{
    public void setPause() {
        Globals.pause = true;
        Time.timeScale = 0;
    }

    public void setResume() {
        Globals.pause = false;
        Time.timeScale = 1;
    }
}
