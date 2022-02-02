using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevelUnlock : MonoBehaviour
{

    private void Awake() {
        PlayerSave s = PlayerSave.loadSettings();

        if (s.Level1Played == true) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }


}
