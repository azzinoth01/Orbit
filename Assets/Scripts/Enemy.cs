using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;



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


    public int shootsToCreate;

    public List<Skillsequenze> skillsequenze;
    private GameObject nextSkill;
    private float nextSkillTime;
    private float nextSkillTimer;
    private int skillIndex;

    private void Awake() {
        nextSkill = skillsequenze[0].Skill;
        nextSkillTime = skillsequenze[0].Delay;
        skillIndex = 0;
        preCreateSkill();
    }
    // Start is called before the first frame update
    void Start() {
        maxDuration = 0;
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
            delayToNextWaypoint = designer.enemyDelayToNextWaypoint;
        }
        catch {
            //       Debug.Log("no designer mode");
        }
        waypointIndex = 0;

        nextSkill = skillsequenze[0].Skill;
        nextSkillTime = skillsequenze[0].Delay;
        skillIndex = 0;

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


            if (nextSkillTime <= nextSkillTimer) {

                activateSkill(false);
                nextSkillTimer = 0;
            }

            if (waypointDirectionSet == false) {
                body.velocity = body.velocity * slowdownProzent;
            }
            if (maxDuration != 0 && time >= maxDuration) {
                Destroy(gameObject.transform.parent.gameObject);

            }
            nextSkillTimer = nextSkillTimer + Time.deltaTime;
            time = time + Time.deltaTime;

        }
    }
    private void preCreateSkill() {
        for (int i = 0; i < shootsToCreate;) {

            GameObject skill = activateSkill(true);
            skill.SetActive(false);


            i = i + 1;
        }
    }

    private GameObject activateSkill(bool preCreation) {
        GameObject skill;
        if (preCreation == false) {
            skill = Globals.bulletPool.Find(x => x.name == nextSkill.name && x.activeSelf == false);
            if (skill == null) {
                skill = Instantiate(nextSkill, transform.position, Quaternion.identity);
                skill.name = nextSkill.name;
                skill.layer = gameObject.layer - 1; // enemy bullet layer ist immer enemy layer -1
                skill.GetComponent<Skill>().layerChange();
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(skill);
                skill.transform.position = transform.position;
                skill.transform.rotation = Quaternion.identity;
                skill.layer = gameObject.layer - 1;
                skill.SetActive(true);


            }

        }
        else {
            skill = Instantiate(nextSkill);
            skill.name = nextSkill.name;
            skill.layer = gameObject.layer - 1;

        }
        skillIndex = skillIndex + 1;

        if (skillIndex == skillsequenze.Count) {
            skillIndex = 0;
        }

        nextSkill = skillsequenze[skillIndex].Skill;
        nextSkillTime = skillsequenze[skillIndex].Delay;

        return skill;
    }

    private void movement() {



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

    public void takeDmg(int dmg) {
        health = health - dmg;

        if (health <= 0) {
            Destroy(gameObject.transform.parent.gameObject);
            Globals.gameoverHandler.gameOver();
        }
    }

    private void resetTime() {
        if (maxDuration != 0) {
            maxDuration = maxDuration + time;
        }
        time = 0;
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
                resetTime();
                //body.velocity = Vector2.zero;
            }
        }
        catch {
            //Debug.Log("no Waypoint collision");
            //Debug.Log(collision);
        }
    }
}
