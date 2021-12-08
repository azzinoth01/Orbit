using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// classe um Globale variablen zu initzalisieren
/// </summary>
public class Globals_init : MonoBehaviour
{
    /// <summary>
    /// initzalisiert die Gloablen variablen
    /// </summary>
    private void Awake() {
        Globals.pause = false;

        if (Globals.bulletPool == null) {
            Globals.bulletPool = new List<Skill>();
        }
        if (Globals.spawnerListe == null) {
            Globals.spawnerListe = new List<Enemy_Spawner>();
        }

    }

}
