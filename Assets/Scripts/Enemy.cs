using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<GameObject> shootingPoints;
    public GameObject bullet;
    public float reloadTime;
    public Rigidbody2D body;
    private Waypoint_Designer designer;
    public List<Vector2> waypoints;
    public float speed;
    public float slowdownProzent;
    public bool loop;
    private float restartAfter;
    public GameObject waypointPrefab;
    private float restartTime;
    private float time;
    private List<GameObject> waypointObject;
    private int waypointIndex;
    private bool waypointDirectionSet;
    public float maxDuration;
    public float delayToNextWaypoint;

    private float reloadTimer;


    // Start is called before the first frame update
    void Start() {
        maxDuration = 0;
        restartTime = 0;
        time = 0;
        reloadTimer = 0;
        waypointObject = new List<GameObject>();
        try {
            designer = GetComponentInParent<Waypoint_Designer>();

            waypoints = new List<Vector2>(designer.waypoints);
            speed = designer.speed;
            loop = designer.loop;
            restartAfter = designer.restartAfter;
            waypointPrefab = designer.waypointPrefab;
            delayToNextWaypoint = designer.enemyDelayToNextWaypoint;
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
            if (delayToNextWaypoint <= time) {
                movement();
            }
            if (reloadTime <= reloadTimer) {
                shoot();
                reloadTimer = 0;
            }

            if (waypointDirectionSet == false) {
                body.velocity = body.velocity * slowdownProzent;
            }
            if (maxDuration != 0 && time >= maxDuration) {
                Destroy(gameObject.transform.parent.gameObject);
                Destroy(gameObject);
            }

            time = time + Time.deltaTime;
            reloadTimer = reloadTimer + Time.deltaTime;
        }
    }
    private void movement() {


        resetTime();
        if (waypoints.Count > waypointIndex && waypointDirectionSet == false) {
            createNextWaypoint(waypoints[waypointIndex]);
            waypointDirectionSet = true;
            Vector2 direction = waypointObject[0].transform.position - transform.position;
            body.velocity = direction.normalized * speed;
        }
        else if (waypoints.Count == waypointIndex && loop == true) {
            createNextWaypoint(new Vector2(0, 0));
            waypointDirectionSet = true;
            Vector2 direction = waypointObject[0].transform.position - transform.position;
            body.velocity = direction.normalized * speed;
            waypointIndex = -1;

        }
        else if (designer != null && waypoints.Count == waypointIndex) {

            //if (loop == true) {
            //    createNextWaypoint(new Vector2(0, 0));
            //    waypointDirectionSet = true;
            //    Vector2 direction = waypointObject[0].transform.position - transform.position;
            //    body.velocity = direction.normalized * speed;
            //    waypointIndex = -1;
            //    return;
            //}
            restartTime = restartTime + Time.deltaTime;
            if (restartAfter <= restartTime) {
                restartTime = 0;
                transform.position = new Vector3(0, 0, transform.position.z);
                waypointIndex = 0;
                waypointDirectionSet = false;

            }
        }




    }

    private void resetTime() {
        if (maxDuration != 0) {
            maxDuration = maxDuration + time;
        }
        time = 0;
    }

    private void shoot() {
        foreach (GameObject w in shootingPoints) {
            GameObject shootholder = new GameObject(w.name + " Enemy shoot");
            shootholder.transform.position = w.transform.position;
            shootholder.transform.eulerAngles = w.transform.eulerAngles;
            GameObject g = Instantiate(bullet, shootholder.transform);
        }
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
                //body.velocity = Vector2.zero;
            }
        }
        catch {
            Debug.Log("no Waypoint collision");
            Debug.Log(collision);
        }
    }
}
