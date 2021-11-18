using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Controlls.IBullet_hellActions
{
    public int health;
    public Rigidbody2D body;

    public float force;
    public float maxSpeed;
    public Pause_handler pause_handler;
    public List<GameObject> weapons;
    public GameObject bullet;
    public float reloadTime;
    private float time;
    private Vector2 impulse;

    private Controlls controll;
    private bool shooting;

    public void OnCharge(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
    }

    public void OnMove_down(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.down * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.down * force);
        }

    }

    public void OnMove_left(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.left * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.left * force);
        }
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.right * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.right * force);
        }
    }

    public void OnMove_up(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.up * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.up * force);
        }
    }

    public void OnShoot(InputAction.CallbackContext context) {

        if (context.started) {
            shooting = true;
        }
        else if (context.canceled) {
            shooting = false;
        }
    }

    void Controlls.IBullet_hellActions.OnPause(InputAction.CallbackContext context) {
        if (Globals.pause == true) {
            pause_handler.setResume();
        }
        else {
            pause_handler.setPause();
        }

    }

    // Start is called before the first frame update
    void Start() {
        if (controll == null) {
            controll = new Controlls();
            controll.Enable();
            controll.bullet_hell.SetCallbacks(this);
        }
        impulse = new Vector2(0, 0);
        shooting = false;
        time = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            time = time + Time.deltaTime;
            if (shooting == true && weapons.Count != 0) {
                if (time >= reloadTime) {
                    time = 0;
                    foreach (GameObject w in weapons) {
                        GameObject shootholder = new GameObject(w.name + " shoot");
                        shootholder.transform.position = w.transform.position;
                        shootholder.transform.eulerAngles = w.transform.eulerAngles;
                        GameObject g = Instantiate(bullet, shootholder.transform);
                    }

                }
            }
            //Debug.Log(impulse);

            //Debug.Log(impulse.normalized * force);
            body.AddForce(impulse.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
        }

    }


    public void takeDmg(int dmg) {
        health = health - dmg;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
