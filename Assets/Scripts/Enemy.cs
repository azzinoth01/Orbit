using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class die enemys beschreibt und deren movement
/// </summary>
public class Enemy : MonoBehaviour
{
    public float health;
    private float maxHealth;



    public Rigidbody2D body;
    private Waypoint_Designer designer;
    public List<Vector2> waypoints;
    public bool moveToRandomWaypoints;
    public bool moveToPlayer;
    public bool followPlayerMovementX;
    public bool followPlayerMovementY;
    public float playerfollowRange;
    public float force;
    public float maxSpeed;

    public bool loop;
    private float restartAfter;
    public GameObject waypointPrefab;
    private float restartTime;
    private List<GameObject> waypointObject;
    private int waypointIndex;


    public float maxDuration;
    public float delayToNextWaypoint;





    public int collisionDmg;
    public bool destoryAfterCollison;

    public GameObject deathParticelSystem;


    private Vector2 savedDirection;
    private bool stopMove;
    private bool maxDurationReached;


    private Enemy_Spawner spawnerCallback;

    public bool showBossHp;
    public Image bossHp;
    public GameObject bossUI;

    //public bool rotateTowardsPlayer;
    //public bool rotateSpeed;




    public Enemy_Spawner SpawnerCallback {
        get {
            return spawnerCallback;
        }

        set {
            spawnerCallback = value;
        }
    }


    /// <summary>
    /// erstellt alle gegner wegpunkte aus Vectoren Liste
    /// startet die max Duration Corutine
    /// und setzt values anhand des wegpunkt designers, wenn am überobject vorhanden ist ( nur für wegpunkt design zwecken)
    /// </summary>
    void Start() {

        maxHealth = health;
        restartTime = 0;


        stopMove = false;

        waypointObject = new List<GameObject>();
        try {
            designer = GetComponentInParent<Waypoint_Designer>();

            waypoints = new List<Vector2>(designer.waypoints);
            force = designer.force;
            maxSpeed = designer.speed;
            loop = designer.loop;
            restartAfter = designer.restartAfter;
            waypointPrefab = designer.waypointPrefab;
            delayToNextWaypoint = designer.enemyDelayToNextWaypoint;
            maxDuration = 0;
        }
        catch {
            //       Debug.Log("no designer mode");
        }
        waypointIndex = 0;





        for (int i = 0; i < waypoints.Count;) {
            createNextWaypoint(waypoints[i]);
            i = i + 1;
        }
        savedDirection = Vector2.zero;

        if (maxDuration != 0) {
            StartCoroutine(startMaxDurationTimer(maxDuration));
        }


        try {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = false;
        }
        catch {

        }


        if (showBossHp == true) {

            bossUI = Globals.bossUI;

            bossHp = Globals.bossHpBar;

            bossUI.SetActive(true);
            StartCoroutine(smoothHealthDrop());
        }




    }
    /// <summary>
    /// movement control
    /// </summary>
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            if (stopMove == false) {
                movement();
            }

            if (maxDurationReached == true) {
                if (moveToPlayer == true || followPlayerMovementX == true || followPlayerMovementY == true || (waypoints.Count == waypointIndex && loop == false && waypoints.Count != 0)) {
                    // sofort rausbewegen bei den anderen wird es erst am wegpunkt gemacht
                    startMovingOut();
                }


            }
        }
    }

    private IEnumerator smoothHealthDrop() {
        while (true) {
            if (Globals.pause == true) {
                yield return null;
            }
            float prozentValue = health / maxHealth;
            float currentFillProzent = bossHp.fillAmount;

            //Debug.Log("% Value " + prozentValue.ToString());
            //Debug.Log("Fill Value " + currentFillProzent.ToString());
            if (prozentValue <= currentFillProzent) {

                float toSet = currentFillProzent - 0.01f;
                if (toSet < prozentValue) {
                    toSet = prozentValue;
                }
                bossHp.fillAmount = toSet;
            }
            else if (prozentValue >= currentFillProzent) {
                float toSet = currentFillProzent + 0.01f;
                if (toSet > prozentValue) {
                    toSet = prozentValue;
                }
                bossHp.fillAmount = toSet;
            }


            if (bossHp.fillAmount <= 0) {
                Destroy(gameObject.transform.parent.gameObject);
                Instantiate(deathParticelSystem, transform.position, transform.rotation);
            }




            yield return null;
        }

    }




    /// <summary>
    /// timer für delay zwischen den einzelen wegpunkt bewegungen
    /// </summary>
    /// <param name="wait"> delay zeit in sekunden</param>
    /// <returns></returns>
    private IEnumerator startMoveDelay(float wait) {
        yield return new WaitForSeconds(wait);

        stopMove = false;

    }
    /// <summary>
    /// timer der die maximale duration des gegner beschreibt
    /// </summary>
    /// <param name="wait">maximale duration in Sekunden</param>
    /// <returns></returns>
    private IEnumerator startMaxDurationTimer(float wait) {
        yield return new WaitForSeconds(wait);

        maxDurationReached = true;
    }

    /// <summary>
    /// startet das rausmoven des gegners, wenn der enemy seine max Duration erreicht hat
    /// deactiviert das enemy script
    /// </summary>
    public void startMovingOut() {
        //Debug.Log(transform.parent.gameObject.name);

        try {
            Move_in_out_Scene m = GetComponentInParent<Move_in_out_Scene>();
            // speed auf anderen rigidbody übergbene 
            m.body.bodyType = RigidbodyType2D.Dynamic;
            m.body.velocity = body.velocity;

            // weil sich sonst das schiff nicht mitbewegt
            //body.bodyType = RigidbodyType2D.Kinematic;
            //Debug.Log("versuch zu rausbewegung zu starten");
            m.startMoveOut();
            enabled = false;
        }
        catch {
            //kein moveout script vorhanden zerstöre enemy an dieser Stelle
            Destroy(transform.parent.gameObject);
        }

    }


    /// <summary>
    /// führt das bewegungsverhalten des enemys durch anhand der gesetzten Variablen
    /// Priorität der Variablen
    /// moveToPlayer
    /// followPlayerMovementX und followPlayerMovementY
    /// Waypoint Liste
    /// moveToRandom Waypoint
    /// </summary>
    private void movement() {

        if (moveToPlayer == true && Globals.player != null) {

            Vector2 direction;
            if (savedDirection == Vector2.zero) {
                direction = Globals.player.transform.position - transform.position;
                savedDirection = direction;
            }
            else {
                direction = savedDirection;
            }

            if (followPlayerMovementX == true && followPlayerMovementY == true && Globals.player != null) {
                //waypointDirectionSet = false;

                direction = Globals.player.transform.position - transform.position;

            }
            else if (followPlayerMovementX == true && Globals.player != null) {
                //waypointDirectionSet = false;

                if (Globals.player.transform.position.x - transform.position.x >= -playerfollowRange && Globals.player.transform.position.x - transform.position.x <= playerfollowRange) {
                    //return;
                }
                else {
                    direction = new Vector2(Globals.player.transform.position.x - transform.position.x, direction.y);

                }

            }
            else if (followPlayerMovementY == true && Globals.player != null) {
                //waypointDirectionSet = false;
                if (Globals.player.transform.position.y - transform.position.y >= -playerfollowRange && Globals.player.transform.position.y - transform.position.y <= playerfollowRange) {
                    //return;
                }
                else {
                    direction = new Vector2(direction.x, Globals.player.transform.position.y - transform.position.y);
                }
            }

            body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));

        }
        else if (followPlayerMovementX == true && Globals.player != null) {
            Vector2 direction;
            if (Globals.player.transform.position.x - transform.position.x >= -playerfollowRange && Globals.player.transform.position.x - transform.position.x <= playerfollowRange) {
                return;
            }
            else {
                direction = new Vector2(Globals.player.transform.position.x - transform.position.x, 0);
            }
            body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
        }
        else if (followPlayerMovementY == true && Globals.player != null) {

            Vector2 direction;
            if (Globals.player.transform.position.y - transform.position.y >= -playerfollowRange && Globals.player.transform.position.y - transform.position.y <= playerfollowRange) {
                return;
            }
            else {
                direction = new Vector2(0, Globals.player.transform.position.y - transform.position.y);
            }
            body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
        }
        else if (waypoints.Count > waypointIndex) {
            //createNextWaypoint(waypoints[waypointIndex]);
            //waypointDirectionSet = true;
            Vector2 direction = waypointObject[waypointIndex].transform.position - transform.position;
            body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
            waypointObject[waypointIndex].SetActive(true);
        }
        else if (waypoints.Count == waypointIndex && loop == true && waypoints.Count != 0) {
            //createNextWaypoint(new Vector2(0, 0));
            //waypointDirectionSet = true;
            waypointIndex = 0;
            Vector2 direction = waypointObject[waypointIndex].transform.position - transform.position;

            body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);

            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));

            waypointObject[waypointIndex].SetActive(true);


        }

        else if (designer != null && waypoints.Count == waypointIndex) {


            restartTime = restartTime + Time.deltaTime;
            if (restartAfter <= restartTime) {
                restartTime = 0;
                waypointIndex = 0;
                transform.position = waypointObject[waypointIndex].transform.position;
                waypointObject[waypointIndex].SetActive(true);


            }
        }




    }

    /// <summary>
    /// take dmg funktion
    /// </summary>
    /// <param name="dmg"> den dmg den der enemy nehmen soll</param>
    public void takeDmg(float dmg) {
        //Debug.Log(dmg);
        //  Debug.Log(health);
        health = health - dmg;
        //  Debug.Log(health);

        if (showBossHp == false) {
            if (health <= 0) {
                Destroy(gameObject.transform.parent.gameObject);
                Instantiate(deathParticelSystem, transform.position, transform.rotation);


                //  Globals.gameoverHandler.gameOver();
            }
        }

    }




    /// <summary>
    /// erzeugt wegpunkte anhand der übergebenen Vectoren
    /// </summary>
    /// <param name="v2"> position für den zu erstellenden Wegpunkt </param>
    private void createNextWaypoint(Vector2 v2) {
        GameObject g = Instantiate(waypointPrefab, transform.parent);
        g.transform.localPosition = v2;
        // g.layer = gameObject.layer;
        waypointObject.Add(g);
        g.SetActive(false);

    }

    /// <summary>
    /// check ob ein wegpunkt erreicht wurde und starte bewegung zum nächsten wegpunkt mit delay, wenn vorhanden
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        try {

            if (collision.gameObject == waypointObject[waypointIndex]) {
                if (moveToRandomWaypoints == true) {
                    int i = -1;
                    //Debug.Log("random Roll start");
                    while (i == -1 || waypointObject.Count == i || i == waypointIndex) {
                        // grenzen um 1 Zahl erweiter da eck zaheln nur selten ausgewürfelt werden
                        // wenn unmögliche Zahl kommt wird zahl neu ermittelt
                        i = Random.Range(-1, waypointObject.Count + 1);
                        //Debug.Log("random roll");
                    }
                    waypointIndex = i;


                }
                else {
                    waypointIndex = waypointIndex + 1;
                }


                collision.gameObject.SetActive(false);
                stopMove = true;
                // nachdem
                if (maxDurationReached == true) {

                    startMovingOut();

                }
                else {

                    StartCoroutine(startMoveDelay(delayToNextWaypoint));
                }
            }
        }
        catch {
            //Debug.Log("no Waypoint collision");
            //Debug.Log(collision);
        }
    }

    /// <summary>
    /// bei collison dmg den player geben
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision) {
        try {
            if (collision.gameObject == Globals.player) {
                Player p = Globals.player.GetComponent<Player>();


                p.takeDmg(collisionDmg);

                if (destoryAfterCollison == true) {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
        catch {
            //Debug.Log(collision);
        }

    }


    /// <summary>
    /// beim zerstören wegpunkte zerstören
    /// win condition enemy kill hinzufügen
    /// callback zum spawner machen, damit dieser neue gegner spawner können
    /// </summary>
    private void OnDestroy() {
        try {
            foreach (GameObject g in waypointObject) {
                Destroy(g);
            }
        }
        catch {

        }

        if (Globals.currentWinCondition != null) {
            Globals.currentWinCondition.enemyKilled();
        }

        if (spawnerCallback != null) {
            spawnerCallback.spawnKilled();
        }
    }
}
