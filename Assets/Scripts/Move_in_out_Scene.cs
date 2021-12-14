using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// classe die sich drum k�mmert, dass enemys aus den Spielfeld rein und raus bewegt
/// </summary>
public class Move_in_out_Scene : MonoBehaviour
{
    public List<Vector2> moveInWaypoints;
    public List<Vector2> moveOutWaypoints;
    public GameObject waypointPrefab;




    public float force;
    public float maxSpeed;

    public Rigidbody2D body;
    private Rigidbody2D childBody;


    private List<GameObject> waypointInObjects;
    private List<GameObject> waypointOutObjects;
    private int waypointIndex;
    private bool moveIn;


    /// <summary>
    /// erzeugt Wegpunkte um Gegner in die Scene rein und raus moven zu lassen
    /// </summary>
    void Start() {
        waypointInObjects = new List<GameObject>();
        waypointOutObjects = new List<GameObject>();

        for (int i = 0; i < moveInWaypoints.Count;) {
            waypointInObjects.Add(createNextWaypoint(moveInWaypoints[i]));
            i = i + 1;
        }

        for (int i = 0; i < moveOutWaypoints.Count;) {
            waypointOutObjects.Add(createNextWaypoint(moveOutWaypoints[i]));
            i = i + 1;
        }

        moveIn = true;
        waypointIndex = 0;

        childBody = GetComponentInChildren<Enemy>().body;

    }

    /// <summary>
    /// f�hrt das move in und auch das move out aus
    /// </summary>
    void Update() {

        if (Globals.pause == true) {
            return;
        }
        else {

            move();
        }
    }


    /// <summary>
    /// beschreibt wie der enemy in die scene sich reinbewegt anhand der Wegpunkten und rausbewegt
    /// und startet das zers�ren des enemys, wenn der letzte move out punkt getroffen wurde
    /// </summary>
    private void move() {

        if (moveIn == true) {
            if (waypointInObjects.Count > waypointIndex) {
                Vector2 direction = waypointInObjects[waypointIndex].transform.position - transform.position;
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

                Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
                normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
                normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

                body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
                childBody.velocity = body.velocity;
                waypointInObjects[waypointIndex].SetActive(true);
            }
            else {
                Enemy e = GetComponentInChildren<Enemy>();
                e.enabled = true;

                e.body.velocity = body.velocity;
                body.bodyType = RigidbodyType2D.Static;
                enabled = false;
                return;
            }
        }
        else {
            if (waypointOutObjects.Count > waypointIndex) {
                Vector2 direction = waypointOutObjects[waypointIndex].transform.position - transform.position;
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

                Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
                normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
                normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

                body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
                childBody.velocity = body.velocity;
                waypointOutObjects[waypointIndex].SetActive(true);
            }
            else {
                Destroy(gameObject);
                //StartCoroutine(startDestroy());
                //enabled = false;
            }
        }
    }

    /// <summary>
    /// startet das move out
    /// </summary>
    public void startMoveOut() {
        //Debug.Log("startet Move out");
        waypointIndex = 0;
        moveIn = false;
        enabled = true;


    }
    ///// <summary>
    ///// corutine um die enemys zu zerst�ren
    ///// destory in corutine um rechenleistung beim destroyen zu sparen
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator startDestroy() {

    //    foreach (GameObject g in waypointInObjects) {
    //        Destroy(g);
    //        yield return null;
    //    }
    //    foreach (GameObject g in waypointOutObjects) {
    //        Destroy(g);
    //        yield return null;
    //    }
    //    Destroy(gameObject);

    //}

    /// <summary>
    /// erzeugt die Wegpunkte an der sich der enemy bewegt
    /// </summary>
    /// <param name="v2"> position der Wegpunkte</param>
    /// <returns> Wegpunkt der erzeugt wurde</returns>
    private GameObject createNextWaypoint(Vector2 v2) {
        GameObject g = Instantiate(waypointPrefab, transform.parent);
        g.transform.localPosition = v2;
        // g.layer = gameObject.layer;
        //waypointInObjects.Add(g);
        g.SetActive(false);
        return g;
    }


    /// <summary>
    /// pr�ft ob ein Wegpunkt getroffen wurde
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        try {

            if (collision.gameObject == waypointInObjects[waypointIndex]) {

                waypointIndex = waypointIndex + 1;
                collision.gameObject.SetActive(false);
                return;
            }
        }
        catch {

        }
        try {

            if (collision.gameObject == waypointOutObjects[waypointIndex]) {

                waypointIndex = waypointIndex + 1;
                collision.gameObject.SetActive(false);
                return;
            }
        }
        catch {

        }

        try {


            if (collision.tag == Tag_enum.enemy_border.ToString()) {
                if (moveIn == true) {
                    GetComponentInChildren<Enemy_skills>().enabled = true;
                    //Debug.Log(collision);
                }
                //else {
                //    GetComponentInChildren<Enemy_skills>().enabled = false;
                //}

            }

        }
        catch {

        }
    }
    /// <summary>
    /// zerst�rt beim zers�ren des enemys auch die Wegpunkte mit
    /// </summary>
    private void OnDestroy() {
        foreach (GameObject g in waypointInObjects) {
            Destroy(g);
        }
        foreach (GameObject g in waypointOutObjects) {
            Destroy(g);
        }
    }
}
