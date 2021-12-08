using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// classe die beschreibt wie die Bullet fliegt
/// </summary>
public class Bullet : MonoBehaviour
{

    public List<Vector2> waypoints;
    public float speed;
    private Waypoint_Designer designer;
    public Rigidbody2D body;
    public GameObject waypointPrefab;

    private List<GameObject> waypointObject;
    private int waypointIndex;
    private bool waypointDirectionSet;
    private bool loop;
    private float restartAfter;
    private float restartTime;



    private float bulletDmg;

    public float BulletDmg {

        set {
            bulletDmg = value;
        }
    }



    /// <summary>
    /// setzt basiswerte und erzeugt die Wegpunkte an der die Bullet entlanf fliegt
    /// </summary>
    void Start() {
        //Debug.Log("start");
        //restartTime = 0;
        //time = 0;
        //waypointIndex = 0;
        waypointObject = new List<GameObject>();
        try {
            designer = GetComponentInParent<Waypoint_Designer>();

            waypoints = new List<Vector2>(designer.waypoints);
            speed = designer.speed;
            loop = designer.loop;
            restartAfter = designer.restartAfter;
            waypointPrefab = designer.waypointPrefab;
        }
        catch {
            //       Debug.Log("no designer mode");
        }
        if (waypoints[0] != Vector2.zero) {
            waypoints.Insert(0, Vector2.zero);
        }



        for (int i = 0; i < waypoints.Count;) {
            createNextWaypoint(waypoints[i]);
            i = i + 1;
        }

    }

    /// <summary>
    /// controlliert das movement der Bullet
    /// </summary>
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            movement();
        }
    }

    /// <summary>
    /// beschreibt wie sich die bullet anhand der Wegpunkte bewegen soll
    /// </summary>
    private void movement() {
        if (waypoints.Count > waypointIndex && waypointDirectionSet == false) {
            //createNextWaypoint(waypoints[waypointIndex]);
            activeNextWaypoint();
            waypointDirectionSet = true;
            Vector2 direction = waypointObject[waypointIndex].transform.position - transform.position;
            body.velocity = direction.normalized * speed;
        }
        else if (designer != null && waypoints.Count == waypointIndex) {

            if (loop == true) {
                //createNextWaypoint(new Vector2(0, 0));
                waypointIndex = 0;
                activeNextWaypoint();
                waypointDirectionSet = true;
                Vector2 direction = waypointObject[waypointIndex].transform.position - transform.position;
                body.velocity = direction.normalized * speed;

                return;
            }
            restartTime = restartTime + Time.deltaTime;
            if (restartAfter <= restartTime) {
                restartTime = 0;
                transform.position = waypointObject[0].transform.position;
                waypointIndex = 0;
                waypointDirectionSet = false;

            }
        }




    }

    /// <summary>
    /// activiert den nächsten Wegpunkt zu den die Bullet fliegen soll
    /// </summary>
    private void activeNextWaypoint() {
        waypointObject[waypointIndex].SetActive(true);
    }


    /// <summary>
    /// erzeugt die Wegpunkte anhand der übergebenen Vectoren
    /// </summary>
    /// <param name="v2"> position des Wegpunktes</param>
    private void createNextWaypoint(Vector2 v2) {
        GameObject g = Instantiate(waypointPrefab, transform.parent);
        g.transform.localPosition = v2;
        g.SetActive(false);
        // g.layer = gameObject.layer;
        waypointObject.Add(g);

    }

    /// <summary>
    /// bullet hat die Bullet border getroffen und wird deswegen inactiv gesetzt
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.tag == Tag_enum.bullet_border.ToString()) {
            setInactive();
        }

    }

    /// <summary>
    /// überprüft ob die Bullet einen Wegpunkt getroggen wurde 
    /// außerdem wird überprüft ob ein enemy oder player getroffen wurde
    /// und gibt diesen den Bullet dmg als dmg
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        try {

            if (collision.gameObject == waypointObject[waypointIndex]) {
                waypointIndex = waypointIndex + 1;
                waypointDirectionSet = false;
                collision.gameObject.SetActive(false);
                return;
                //Destroy(collision.gameObject);
                //waypointObject.RemoveAt(0);
            }
        }
        catch {
            //Debug.Log("no Waypoint collision");
            //Debug.Log(collision);
        }

        try {

            //Debug.Log(collision);
            Enemy g = collision.GetComponent<Enemy>();
            if (g != null) {
                //Debug.Log(collision);
                g.takeDmg((int)bulletDmg);
                //Destroy(gameObject.transform.parent.gameObject);
                setInactive();
                return;
            }


        }
        catch {
            //Debug.Log("no enemy hit");
            //Debug.Log(collision);
        }

        try {

            //Debug.Log(collision);
            Player g = collision.GetComponent<Player>();
            if (g != null) {
                g.takeDmg((int)bulletDmg);
                //Destroy(gameObject.transform.parent.gameObject);
                setInactive();
                return;
            }


        }
        catch {
            //Debug.Log("no player hit");
            //Debug.Log(collision);
        }


    }

    //private void OnDisable() {
    //    //Debug.Log("bullet got disabled");

    //    if (gameObject.layer == 8) { // layer 8 enemy_bullets

    //        Globals.bulletPool.Add(gameObject.transform.parent.gameObject);

    //    }
    //    else {
    //        Globals.bulletPool.Add(gameObject.transform.parent.gameObject);
    //    }

    //}


    /// <summary>
    /// setzt werte auf basis Werte zurück
    /// </summary>
    private void OnEnable() {
        //Debug.Log("bullet got enabled");
        restartTime = 0;

        waypointIndex = 0;
        if (waypointObject != null) {
            transform.position = waypointObject[0].transform.position;
        }

        waypointDirectionSet = false;

    }

    /// <summary>
    /// setzt die Bullet inactive und aktiviert den skill inactive check
    /// </summary>
    private void setInactive() {

        gameObject.transform.parent.gameObject.SetActive(false);

        //Debug.Log("set inactive");
        gameObject.transform.parent.transform.parent.GetComponent<Skill>().checkDisabled();
    }




}
