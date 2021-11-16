using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public Rigidbody2D body;
    public float speed;
    //  public Vector2 speed;
    private Vector2 transformedSpeed;
    public List<GameObject> waypoints;
    public int waypointIndex;
    private bool waypointDirectionSet;

    //public Vector2 impulse1;
    //public float delay1;
    //private bool trigered1;
    //public Vector2 impulse2;
    //public float delay2;
    //private bool trigered2;
    //public Vector2 impulse3;
    //public float delay3;
    //private bool trigered3;

    public float timer;

    // Start is called before the first frame update
    void Start() {
        waypointIndex = 0;

        //// transform speed angel dependent
        //float c = Mathf.Sqrt((speed.x * speed.x) + (speed.y * speed.y));
        //transformedSpeed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.z) * c, Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.z) * c);

        //body.velocity = transformedSpeed;
        //trigered1 = false;
        //trigered2 = false;
        //trigered3 = false;


        waypointDirectionSet = false;
    }

    // Update is called once per frame
    void Update() {

        if (waypoints.Count > waypointIndex && waypointDirectionSet == false) {
            waypointDirectionSet = true;
            Vector2 direction = waypoints[waypointIndex].transform.position - transform.position;
            //Debug.Log(waypoints[waypointIndex].transform.position);
            //Debug.Log(transform.position);
            //Debug.Log(direction);
            //float angle = Vector2.SignedAngle(Vector2.right, waypoints[waypointIndex].transform.position);

            ////angle = Vector2.SignedAngle(Vector2.right, new Vector2(2, 2));

            //Debug.Log(angle);

            //transformedSpeed = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * speed, Mathf.Sin(Mathf.Deg2Rad * angle) * speed);
            body.velocity = direction.normalized * speed;
        }

        //timer = timer + Time.deltaTime;

        //if (trigered1 == false && delay1 <= timer) {
        //    body.AddRelativeForce(impulse1);
        //    trigered1 = true;
        //}
        //if (trigered2 == false && delay2 <= timer) {
        //    body.AddRelativeForce(impulse2);
        //    trigered2 = true;
        //}
        //if (trigered3 == false && delay2 <= timer) {
        //    body.AddRelativeForce(impulse3);


        //    trigered3 = true;

        //}
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == waypoints[waypointIndex]) {
            waypointIndex = waypointIndex + 1;
            waypointDirectionSet = false;
        }

    }

}
