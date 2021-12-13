using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// camera Movement script
/// </summary>
public class CameraScript : MonoBehaviour
{

    private Rigidbody2D body;
    public Player player;
    private float playerMaxSpeed;
    private Vector2 offset;
    public float offsetSpeed;



    public float screenShakeMaxDuration;
    public float screenShakeMinDuration;

    public float screenShakeMaxMagnitude;

    private Vector2 screenShakeOffset;
    private bool screenShakeRunning;

    /// <summary>
    /// setzt die momentane main camera in den Globalen variablen
    /// </summary>
    private void Awake() {
        Globals.currentCamera = gameObject.GetComponent<Camera>();
    }
    /// <summary>
    /// setzt basis variablen
    /// </summary>
    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        player = Globals.player.GetComponent<Player>();
        playerMaxSpeed = player.maxSpeed;
        offset = Vector2.zero;
    }

    /// <summary>
    /// bewegt die camaera in bewegungsrichtung mit kleinem offset, damit man mehr in der Bewegungsrichtung sieht
    /// </summary>
    private void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            if (player == null) {
                return;
            }
            //Debug.Log((Globals.player.transform.position - transform.position));
            offset = new Vector2((6 * (player.Impulse.x / player.force) * Time.deltaTime) + offset.x, (5 * (player.Impulse.y / player.force) * Time.deltaTime) + offset.y);

            //Vector2 Clampoffset = new Vector2(6 * (player.Impulse.x / player.force), 5 * (player.Impulse.y / player.force));
            Vector2 Clampoffset = new Vector2(6, 5);
            offset = new Vector2(Mathf.Clamp(offset.x, -Clampoffset.x, Clampoffset.x), Mathf.Clamp(offset.y, -Clampoffset.y, Clampoffset.y));




            Vector2 direction = ((Vector2)Globals.player.transform.position - ((Vector2)transform.position - offset)) * playerMaxSpeed;

            body.velocity = player.body.velocity + direction;
            //direction = direction - offset;

            //body.AddForce(direction);

            //body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -playerMaxSpeed, playerMaxSpeed), Mathf.Clamp(body.velocity.y, -playerMaxSpeed, playerMaxSpeed));

        }
    }


    private IEnumerator screenShake() {


        while (screenShakeRunning == true) {


            Vector2 shake = Random.insideUnitCircle * screenShakeMaxMagnitude;




            yield return null;
        }

        transform.position = transform.position - (Vector3)screenShakeOffset;
    }

}
