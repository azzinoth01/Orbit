using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// classe die hilf den Gamedesigner Wegpunktliste zu erzeugen
/// </summary>
public class Waypoint_Designer : MonoBehaviour
{

    public List<Vector2> waypoints;
    public float speed;
    public float force;
    public float restartAfter;
    public bool loop;
    public float enemyDelayToNextWaypoint;
    public LineRenderer line;
    public GameObject waypointPrefab;
    // public GameObject movementCheckPrefab;
    private GameObject waypointPlacer;
    private bool placeWaypoint;
    private int lineIndex;

    public List<GameObject> waypointList;



    /// <summary>
    /// setzt die Vectoren in den Line Render um die Wegpunkt verbindung visuell anzuzeigen
    /// </summary>
    void Start() {
        line.positionCount = waypoints.Count + 1;

        lineIndex = 0;
        // waypointList = new List<GameObject>();
        foreach (Vector2 v2 in waypoints) {
            line.SetPosition(lineIndex + 1, v2);
            lineIndex = lineIndex + 1;

            //  GameObject g = Instantiate(waypointPrefab, v2, Quaternion.identity, transform);

            //waypointList.Add(g);
        }

    }

    /// <summary>
    /// plaziert die Wegpunkte auf den Screen, wenn linker Mousebutton gelickt wird
    /// beim rechten Mousbutton klick wird das setzen der Wegpunkte deaktiviert werden
    /// </summary>
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            if (placeWaypoint == true) {
                waypointPlacer.transform.position = (Vector2)Globals.currentCamera.ScreenToWorldPoint((Vector2)Mouse.current.position.ReadValue());
                if (Mouse.current.leftButton.wasPressedThisFrame) {
                    GameObject g = Instantiate(waypointPlacer, transform);
                    waypoints.Add(g.transform.position);
                    line.positionCount = line.positionCount + 1;
                    line.SetPosition(lineIndex + 1, g.transform.position);
                    lineIndex = lineIndex + 1;

                }
                if (Mouse.current.rightButton.wasPressedThisFrame) {

                    placeWaypoint = false;
                    Destroy(waypointPlacer);
                }
            }
        }

    }


    /// <summary>
    /// aktiviert die Wegpunkt setzung
    /// </summary>
    [ContextMenu("Set Waypoints on Screen")]
    public void paintWayoints() {


        waypointPlacer = Instantiate(waypointPrefab, (Vector3)Mouse.current.position.ReadValue(), Quaternion.identity);
        placeWaypoint = true;

    }
}
