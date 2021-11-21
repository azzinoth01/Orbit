using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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



    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            movement();
        }
    }
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

    private void activeNextWaypoint() {
        waypointObject[waypointIndex].SetActive(true);
    }

    private void createNextWaypoint(Vector2 v2) {
        GameObject g = Instantiate(waypointPrefab, transform.parent);
        g.transform.localPosition = v2;
        g.SetActive(false);
        // g.layer = gameObject.layer;
        waypointObject.Add(g);

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        try {

            if (collision.gameObject == waypointObject[waypointIndex]) {
                waypointIndex = waypointIndex + 1;
                waypointDirectionSet = false;
                collision.gameObject.SetActive(false);
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
            g.takeDmg(1);
            //Destroy(gameObject.transform.parent.gameObject);
            setInactive();

        }
        catch {
            //Debug.Log("no enemy hit");
            //Debug.Log(collision);
        }

        try {

            //Debug.Log(collision);
            Player g = collision.GetComponent<Player>();
            g.takeDmg(1);
            //Destroy(gameObject.transform.parent.gameObject);
            setInactive();

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
    // called vor start
    private void OnEnable() {
        //Debug.Log("bullet got enabled");
        restartTime = 0;

        waypointIndex = 0;
        if (waypointObject != null) {
            transform.position = waypointObject[0].transform.position;
        }

        waypointDirectionSet = false;
    }

    private void setInactive() {

        gameObject.transform.parent.gameObject.SetActive(false);

        //check ob alle anderen bullet container ebenfalls inactive sind
        int i = gameObject.transform.parent.gameObject.transform.parent.transform.childCount;
        int counter = 0;

        foreach (Transform t in gameObject.transform.parent.gameObject.transform.parent.transform) {
            if (t.gameObject.activeSelf == false) {
                counter = counter + 1;
            }
            else if (t.gameObject == gameObject.transform.parent.gameObject) {
                // falls beim durchloopen das momentane object noch nicht als inactive angesehen wird
                counter = counter + 1;
            }
        }
        if (i == counter) {
            gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

}
