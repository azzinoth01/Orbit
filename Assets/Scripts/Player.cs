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

    private Animator anim;
    public Animator antrieb;

    public int additionalDmg;
    public float dmgModifier;

    public float immunityTimeAfterHit;
    public float immunityTimeAfterCharge;



    private void Awake() {
        Globals.player = gameObject;

    }

    public void OnCharge(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
    }

    public void OnMove_down(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.down * force);
            anim.SetInteger("IntY", anim.GetInteger("IntY") - 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") - 1);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.down * force);
            anim.SetInteger("IntY", anim.GetInteger("IntY") + 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") + 1);
        }

    }

    public void OnMove_left(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.left * force);
            anim.SetInteger("IntX", anim.GetInteger("IntX") - 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") - 1);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.left * force);
            anim.SetInteger("IntX", anim.GetInteger("IntX") + 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") + 1);
        }
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.right * force);
            anim.SetInteger("IntX", anim.GetInteger("IntX") + 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") + 1);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.right * force);
            anim.SetInteger("IntX", anim.GetInteger("IntX") - 1);
            antrieb.SetInteger("IntX", antrieb.GetInteger("IntX") - 1);
        }
    }

    public void OnMove_up(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.up * force);
            anim.SetInteger("IntY", anim.GetInteger("IntY") + 1);
            antrieb.SetInteger("IntY", antrieb.GetInteger("IntY") + 1);

        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.up * force);
            anim.SetInteger("IntY", anim.GetInteger("IntY") - 1);
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
        anim = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {



        }

    }
    private void OnEnable() {
        StartCoroutine(shootingHandler());
        StartCoroutine(moveHandler());
    }

    private IEnumerator moveHandler() {
        while (true) {
            if (Globals.pause == false) {
                body.AddForce(impulse.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
                Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
                normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
                normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

                body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
            }

            yield return null;
        }

    }

    private IEnumerator shootingHandler() {


        while (true) {
            if (shooting == true && weapons.Count != 0 && Globals.pause == false) {
                foreach (Weapon w in weapons) {
                    w.shoot(additionalDmg, dmgModifier);
                }
            }
            yield return null;
        }
    }



    public void takeDmg(int dmg) {
        health = health - dmg;

        if (health <= 0) {
            Destroy(gameObject);
            //Globals.gameoverHandler.gameOver();
        }
    }

    private void OnDestroy() {
        //controll.Disable();
        controll.Dispose();

    }



}
