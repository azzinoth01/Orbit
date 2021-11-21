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

    public List<Weapon> weapons;


    private Vector2 impulse;

    private Controlls controll;
    private bool shooting;

    private new Animator animation;
    public Animator antrieb;

    private void Awake() {
        Globals.player = gameObject;

    }

    public void OnCharge(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
    }

    public void OnMove_down(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.down * force);
            animation.SetInteger("IntY", animation.GetInteger("IntY") - 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") - 1);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.down * force);
            animation.SetInteger("IntY", animation.GetInteger("IntY") + 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") + 1);
        }

    }

    public void OnMove_left(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.left * force);
            animation.SetInteger("IntX", animation.GetInteger("IntX") - 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") - 1);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.left * force);
            animation.SetInteger("IntX", animation.GetInteger("IntX") + 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") + 1);
        }
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.right * force);
            animation.SetInteger("IntX", animation.GetInteger("IntX") + 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") + 1);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.right * force);
            animation.SetInteger("IntX", animation.GetInteger("IntX") - 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") - 1);
        }
    }

    public void OnMove_up(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.up * force);
            animation.SetInteger("IntY", animation.GetInteger("IntY") + 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") + 1);

        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.up * force);
            animation.SetInteger("IntY", animation.GetInteger("IntY") - 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") - 1);
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
    public void OnPause_menu(InputAction.CallbackContext context) {
        //  Debug.Log("pause called" + context);

        if (context.started) {
            //    Debug.Log(Globals.pause);
            if (Globals.pause == true) {
                Globals.pauseHandler.setResume();
            }
            else {
                Globals.pauseHandler.setPause();
            }
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
        animation = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {

            if (shooting == true && weapons.Count != 0) {
                foreach (Weapon w in weapons) {
                    w.shoot();
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
            Globals.gameoverHandler.gameOver();
        }
    }

    private void OnDestroy() {
        //controll.Disable();
        controll.Dispose();

    }

}
