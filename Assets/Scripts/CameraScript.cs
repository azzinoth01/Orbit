using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Rigidbody2D body;
    private Player player;
    private float playerMaxSpeed;
    private Vector2 offset;
    public float offsetSpeed;


    private void Awake() {
        Globals.currentCamera = gameObject.GetComponent<Camera>();
    }

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        player = Globals.player.GetComponent<Player>();
        playerMaxSpeed = player.maxSpeed;
        offset = Vector2.zero;
    }


    private void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
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

}
