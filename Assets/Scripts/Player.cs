using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    public float immunityFlickerRate;
    [Range(0, 1)] public float maxFlickerRange;
    private int flickerDirection;

    public float immunityTimeAfterHit;
    public float immunityTimeAfterDoge;

    public int dogeCharges;
    public float dogeRange;
    public float dogeSpeed;
    public float maxDogeDuration;
    public float dogeCooldown;
    public float globalCooldown;

    private int maxDogeCharges;

    private bool onGlobalCooldown;
    private bool isDoging;

    // public List<GameObject> chargeBalls;
    public List<Sprite> chargeSprites;
    public Image chargeUI;

    public GameObject waypointPrefab;
    private GameObject waypoint;
    private Coroutine timer;
    private Coroutine immunityTimer;
    private Coroutine chargeFillCo;
    private Coroutine flickerCo;


    private bool isImmun;
    private SpriteRenderer sp;



    public Vector2 Impulse {
        get {
            return impulse;
        }


    }

    private void Awake() {
        Globals.player = gameObject;


    }

    public void OnCharge(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();

        if (context.started) {
            doge();
        }
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
        onGlobalCooldown = false;
        shooting = false;
        isDoging = false;
        isImmun = false;
        StartCoroutine(shootingHandler());
        StartCoroutine(moveHandler());
        maxDogeCharges = dogeCharges;
        flickerDirection = -1;
        sp = GetComponent<SpriteRenderer>();
    }


    private void flicker() {

        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + (flickerDirection * immunityFlickerRate * Time.deltaTime));

        if (sp.color.a <= maxFlickerRange) {
            flickerDirection = 1;

        }
        else if (sp.color.a >= 1) {
            flickerDirection = -1;
        }

    }

    private IEnumerator moveHandler() {
        while (true) {
            if (Globals.pause == false && isDoging == false) {
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
        if (isImmun == true) {
            return;
        }
        health = health - dmg;

        if (health <= 0) {
            Destroy(gameObject);
            //Globals.gameoverHandler.gameOver();
        }
        isImmun = true;
        gameObject.layer = 13; // immunity layer
        immunityTimer = StartCoroutine(immunityTime(immunityTimeAfterHit));
    }

    private void OnDestroy() {
        //controll.Disable();
        controll.Dispose();

    }

    private IEnumerator chargeFill(float cooldown) {
        yield return new WaitForSeconds(cooldown);

        dogeCharges = dogeCharges + 1;
        dogeVisual(false);
        if (dogeCharges != maxDogeCharges) {
            chargeFillCo = StartCoroutine(chargeFill(cooldown));
        }
        else {
            chargeFillCo = null;
        }
    }

    private IEnumerator globalCooldownTimer(float cooldown) {
        yield return new WaitForSeconds(cooldown);
        onGlobalCooldown = false;
    }

    private void doge() {
        if (dogeCharges > 0 && onGlobalCooldown == false && isDoging == false) {
            isDoging = true;
            isImmun = true;
            gameObject.layer = 13; // immunity layer



            // falls immunity durch hit wird diese vom doge überschrieben
            if (immunityTimer != null) {
                StopCoroutine(immunityTimer);
            }



            //transform.position = transform.position + (Vector3)(impulse.normalized * dogeRange);

            Vector3 point;

            point = transform.position + (Vector3)(impulse.normalized * dogeRange);
            Vector3 cameraPoint = Globals.currentCamera.WorldToViewportPoint(point);
            float fixedDogeRange = dogeRange;
            // check if charge punkt is outside of field
            while (cameraPoint.x < 0 || cameraPoint.x > 1 || cameraPoint.y < 0 || cameraPoint.y > 1) {
                //Debug.Log("doge outside view");

                fixedDogeRange = fixedDogeRange - 1;
                if (fixedDogeRange <= 0) {
                    //Debug.Log("doge nicht möglich");
                    isDoging = false;
                    return;
                }
                point = transform.position + (Vector3)(impulse.normalized * fixedDogeRange);
                cameraPoint = Globals.currentCamera.WorldToViewportPoint(point);

            }
            //Debug.Log("current pos " + transform.position.ToString());
            //Debug.Log("ziel pos " + point.ToString());
            waypoint = Instantiate(waypointPrefab, point, Quaternion.identity, transform.parent);

            Vector3 direction = (waypoint.transform.position - transform.position);
            //Debug.Log(direction);
            //Debug.Log(direction.normalized);
            body.velocity = direction.normalized * dogeSpeed;
            //Debug.Log(body.velocity);

            dogeCharges = dogeCharges - 1;
            dogeVisual(true);
            onGlobalCooldown = true;
            timer = StartCoroutine(maxDogeTimer(maxDogeDuration));

            if (chargeFillCo == null) {
                chargeFillCo = StartCoroutine(chargeFill(dogeCooldown));
            }

            StartCoroutine(globalCooldownTimer(globalCooldown));


        }
    }

    private IEnumerator immunityFlickerHandler() {


        while (isImmun == true) {
            flicker();
            yield return null;
        }
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);

        flickerCo = null;
    }

    private IEnumerator immunityTime(float time) {
        if (flickerCo == null) {
            flickerCo = StartCoroutine(immunityFlickerHandler());
        }
        yield return new WaitForSeconds(time);
        isImmun = false;
        gameObject.layer = 7; //player layer
    }



    private IEnumerator maxDogeTimer(float duration) {
        yield return new WaitForSeconds(duration);
        isDoging = false;
        Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
        normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
        normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

        body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));

        Destroy(waypoint);
        immunityTimer = StartCoroutine(immunityTime(immunityTimeAfterDoge));

    }

    private void dogeVisual(bool used) {
        if (used == true) {

            chargeUI.sprite = chargeSprites[dogeCharges];
            if (dogeCharges == 0) {
                chargeUI.color = new Color(chargeUI.color.r, chargeUI.color.g, chargeUI.color.b, 0);
            }
            else {
                chargeUI.color = new Color(chargeUI.color.r, chargeUI.color.g, chargeUI.color.b, 1);
            }
            //chargeBalls[dogeCharges].GetComponent<Image>().color = Color.red;
        }
        else {
            chargeUI.sprite = chargeSprites[dogeCharges];
            if (dogeCharges == 0) {
                chargeUI.color = new Color(chargeUI.color.r, chargeUI.color.g, chargeUI.color.b, 0);
            }
            else {
                chargeUI.color = new Color(chargeUI.color.r, chargeUI.color.g, chargeUI.color.b, 1);
            }
            //chargeBalls[dogeCharges].GetComponent<Image>().color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject == waypoint) {
            //Debug.Log("doge complete");
            StopCoroutine(timer);
            isDoging = false;
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));

            Destroy(waypoint);
            immunityTimer = StartCoroutine(immunityTime(immunityTimeAfterDoge));
        }
    }

}
