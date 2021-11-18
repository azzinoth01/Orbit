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
    public int maxDuration;
    private List<GameObject> waypointObject;
    private int waypointIndex;
    private bool waypointDirectionSet;
    private bool loop;
    private float restartAfter;
    private float restartTime;
    private float time;



    // Start is called before the first frame update
    void Start() {
        restartTime = 0;
        time = 0;
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
        waypointIndex = 0;

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
            createNextWaypoint(waypoints[waypointIndex]);
            waypointDirectionSet = true;
            Vector2 direction = waypointObject[0].transform.position - transform.position;
            body.velocity = direction.normalized * speed;
        }
        else if (designer != null && waypoints.Count == waypointIndex) {

            if (loop == true) {
                createNextWaypoint(new Vector2(0, 0));
                waypointDirectionSet = true;
                Vector2 direction = waypointObject[0].transform.position - transform.position;
                body.velocity = direction.normalized * speed;
                waypointIndex = -1;
                return;
            }
            restartTime = restartTime + Time.deltaTime;
            if (restartAfter <= restartTime) {
                restartTime = 0;
                transform.position = new Vector3(0, 0, transform.position.z);
                waypointIndex = 0;
                waypointDirectionSet = false;

            }
        }


        if (maxDuration != 0 && time >= maxDuration) {
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
        time = time + Time.deltaTime;
    }


    private void createNextWaypoint(Vector2 v2) {
        GameObject g = Instantiate(waypointPrefab, transform.parent);
        g.transform.localPosition = v2;
        // g.layer = gameObject.layer;
        waypointObject.Add(g);

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        try {

            if (collision.gameObject == waypointObject[0]) {
                waypointIndex = waypointIndex + 1;
                waypointDirectionSet = false;
                Destroy(collision.gameObject);
                waypointObject.RemoveAt(0);
            }
        }
        catch {
            Debug.Log("no Waypoint collision");
            Debug.Log(collision);
        }
    }



}
