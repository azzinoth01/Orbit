using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

/// <summary>
/// classe des Spielers
/// implementiert das Controler Interface
/// </summary>
public class Player : MonoBehaviour, Controlls.IBullet_hellActions
{
    public float health;
    private float currentHealth;
    public Image healthbar;

    public Color healthbarAbove60;
    public Color healthbarAbove30;
    public Color healthbarBelow30;

    public float maxschield;
    public float currentschield;
    public Image schieldbar;
    public float schieldbarStepValue;

    public float schieldRefreshRate;
    public float schieldRefreshValue;


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


    /// <summary>
    /// setzt den Player in die Globalen variablen
    /// </summary>
    private void Awake() {
        Globals.player = gameObject;


    }

    /// <summary>
    /// controler action für den doge befehl
    /// </summary>
    /// <param name="context"></param>
    public void OnDoge(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();

        if (context.started) {
            doge();
        }
    }

    /// <summary>
    /// controler action für den move down befehl
    /// </summary>
    /// <param name="context"></param>
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
        //Debug.Log("move down");
    }
    /// <summary>
    /// controler action für den move left befehl
    /// </summary>
    /// <param name="context"></param>
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
    /// <summary>
    /// controler action für den move right befehl
    /// </summary>
    /// <param name="context"></param>
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
    /// <summary>
    /// controler action für den move up befehl
    /// </summary>
    /// <param name="context"></param>
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

    /// <summary>
    /// controler action für den shoot befehl
    /// </summary>
    /// <param name="context"></param>
    public void OnShoot(InputAction.CallbackContext context) {

        if (context.started) {
            shooting = true;
        }
        else if (context.canceled) {
            shooting = false;
        }
    }
    /// <summary>
    /// controler action für das Pause Menu
    /// </summary>
    /// <param name="context"></param>
    public void OnPause_menu(InputAction.CallbackContext context) {
        //  Debug.Log("pause called" + context);

        if (context.started) {
            //    Debug.Log(Globals.pause);
            if (Globals.pause == true) {
                Globals.menuHandler.setResume();
            }
            else {
                Globals.menuHandler.setPause();
            }
        }
    }


    /// <summary>
    /// erzeugt das Controler Object und lädt alle rebinds
    /// </summary>
    void Start() {

        if (controll == null) {
            controll = new Controlls();

            Rebinding_menu rebind = new Rebinding_menu();
            controll = rebind.loadRebinding(controll);

            controll.bullet_hell.Enable();
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
    /// <summary>
    /// startet jegliche benötigte corutine
    /// </summary>
    private void OnEnable() {
        onGlobalCooldown = false;
        shooting = false;
        isDoging = false;
        isImmun = false;

        currentHealth = health;
        //currentschield = maxschield;
        StartCoroutine(shootingHandler());
        StartCoroutine(moveHandler());
        StartCoroutine(smoothHealthDrop());
        StartCoroutine(smoothSchieldDrop());
        StartCoroutine(schieldRefresh(schieldRefreshRate));

        maxDogeCharges = dogeCharges;
        flickerDirection = -1;
        sp = GetComponent<SpriteRenderer>();


    }


    /// <summary>
    /// funktion die einen flickering effekt erzeugt
    /// </summary>
    private void flicker() {

        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + (flickerDirection * immunityFlickerRate * Time.deltaTime));

        if (sp.color.a <= maxFlickerRange) {
            flickerDirection = 1;

        }
        else if (sp.color.a >= 1) {
            flickerDirection = -1;
        }

    }


    /// <summary>
    /// corutine die das bewegen handelt
    /// speed wird normalized damit die maximale geschwindingkeit einen Perfecten Kreis bildet, 
    /// damit eine schräge bewegung nicht schneller ist als eine hoch/runter/links/rechts bewegung
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// corutine die dauerhaft prüft ob der Shoot button gedrückt wird und schüsse der Waffe abfeuert
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// corutine die langsam das leben dropt, wenn man schaden genommen hat, damit das leben flüssig nach unten geht
    /// </summary>
    /// <returns></returns>
    public IEnumerator smoothHealthDrop() {

        while (true) {
            float prozentValue = currentHealth / health;
            float currentFillProzent = healthbar.fillAmount;

            //Debug.Log("% Value " + prozentValue.ToString());
            //Debug.Log("Fill Value " + currentFillProzent.ToString());
            if (prozentValue <= currentFillProzent) {

                float toSet = currentFillProzent - 0.01f;
                if (toSet < prozentValue) {
                    toSet = prozentValue;
                }
                healthbar.fillAmount = toSet;
            }
            else if (prozentValue >= currentFillProzent) {
                float toSet = currentFillProzent + 0.01f;
                if (toSet > prozentValue) {
                    toSet = prozentValue;
                }
                healthbar.fillAmount = toSet;
            }

            if (healthbar.fillAmount >= 0.6f) {
                healthbar.color = healthbarAbove60;
            }
            else if (healthbar.fillAmount >= 0.3f) {
                healthbar.color = healthbarAbove30;
            }
            else {
                healthbar.color = healthbarBelow30;
            }

            yield return null;
        }
    }


    /// <summary>
    /// gleich wie beim healthdrop nur fürs schild
    /// </summary>
    /// <returns></returns>
    public IEnumerator smoothSchieldDrop() {

        while (true) {

            float prozentValue = currentschield / maxschield;
            float currentFillProzent = schieldbar.fillAmount;

            if (prozentValue <= currentFillProzent) {
                float toSet = currentFillProzent - schieldbarStepValue;
                if (toSet < prozentValue) {
                    toSet = prozentValue;
                }
                schieldbar.fillAmount = toSet;
            }
            else if (prozentValue >= currentFillProzent) {
                float toSet = currentFillProzent + schieldbarStepValue;
                if (toSet > prozentValue) {
                    toSet = prozentValue;
                }
                schieldbar.fillAmount = toSet;
            }

            if (schieldbar.fillAmount <= 0) {
                // startet das Schildauffüllen nur nachdem das schild leer ist
                StartCoroutine(schieldRefresh(schieldRefreshRate));
            }

            yield return null;
        }
    }

    /// <summary>
    /// corutine die das Schild aufläd
    /// </summary>
    /// <param name="wait">Zeit zwischen den Schild wert erhöhungen</param>
    /// <returns></returns>
    public IEnumerator schieldRefresh(float wait) {



        yield return new WaitForSeconds(wait);

        currentschield = currentschield + schieldRefreshValue;

        if (currentschield >= maxschield) {
            currentschield = maxschield;
        }
        else {
            StartCoroutine(schieldRefresh(wait));
        }


    }

    /// <summary>
    /// take dmg funktion
    /// </summary>
    /// <param name="dmg"> den dmg den der player nehmen soll</param>
    public void takeDmg(int dmg) {
        if (isImmun == true) {
            return;
        }

        if (schieldbar.fillAmount >= 1) {
            currentschield = 0;

            // verschoben damit erst startet sobald anzeige wirklich auf 0 gedroped ist
            //StartCoroutine(schieldRefresh(schieldRefreshRate));

        }
        else {
            currentHealth = currentHealth - dmg;
        }


        if (currentHealth <= 0) {
            Destroy(gameObject);
            Globals.menuHandler.setGameOver();
            //Globals.gameoverHandler.gameOver();
        }
        isImmun = true;
        gameObject.layer = (int)Layer_enum.player_immunity; // immunity layer
        immunityTimer = StartCoroutine(immunityTime(immunityTimeAfterHit));
    }

    /// <summary>
    /// destroys den controller input, weil sonst würde der bestehen bleiben nachdem der Spieler zerstört wurde
    /// </summary>
    private void OnDestroy() {

        controll.Dispose();

    }

    /// <summary>
    /// destroys den controller input
    /// </summary>
    public void clearControlls() {

        controll.Dispose();

    }

    /// <summary>
    /// charge refill timer
    /// </summary>
    /// <param name="cooldown">cooldwon des charges in sekunden </param>
    /// <returns></returns>
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

    /// <summary>
    /// cooldown Timer zwischen spezailfähigkeiten damit diese nicht gespammt werden können
    /// </summary>
    /// <param name="cooldown"> zeit zwischen spezialfähigkeiten in sekunden</param>
    /// <returns></returns>
    private IEnumerator globalCooldownTimer(float cooldown) {
        yield return new WaitForSeconds(cooldown);
        onGlobalCooldown = false;
    }

    /// <summary>
    /// doge spezialfähigkeit, welche den spieler für kurze zeit in eine gewise richtung beschleunigt
    /// </summary>
    private void doge() {
        if (dogeCharges > 0 && onGlobalCooldown == false && isDoging == false && impulse != Vector2.zero) {
            isDoging = true;
            isImmun = true;
            gameObject.layer = (int)Layer_enum.player_immunity; // immunity layer



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
    /// <summary>
    /// corutine die das flickern vom spieler darstellt wenn er schadens immun ist
    /// </summary>
    /// <returns></returns>
    private IEnumerator immunityFlickerHandler() {


        while (isImmun == true) {
            flicker();
            yield return null;
        }
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);

        flickerCo = null;
    }

    /// <summary>
    /// timer für das immun sein des Spielers
    /// </summary>
    /// <param name="time"> dauer der immunity des spielers in sekunden</param>
    /// <returns></returns>
    private IEnumerator immunityTime(float time) {
        if (flickerCo == null) {
            flickerCo = StartCoroutine(immunityFlickerHandler());
        }
        yield return new WaitForSeconds(time);
        isImmun = false;
        gameObject.layer = (int)Layer_enum.player; //player layer
    }


    /// <summary>
    /// timer der das dogen abbricht, wenn der Zielpunkt nicht erreicht wird
    /// </summary>
    /// <param name="duration">die dauer in sekunden</param>
    /// <returns></returns>
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

    /// <summary>
    /// visualisiert die charge anzeige für das dogen
    /// </summary>
    /// <param name="used">variable die beschreibt ob ein charge benutzt wurde oder nicht</param>
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

    /// <summary>
    /// check ob der doge zielpunkt erreich wurde
    /// </summary>
    /// <param name="collision"></param>
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
