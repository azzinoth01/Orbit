using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals_init : MonoBehaviour
{
    //public bool pause;
    private void Awake() {
        Globals.pause = false;

        if (Globals.bulletPool == null) {
            Globals.bulletPool = new List<GameObject>();
        }

    }

}
