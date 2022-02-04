using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_rotation : MonoBehaviour
{
    public bool rotateTowardsPlayer;
    public float rotateSpeed;

    public bool activatedAfterMoveIn;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start() {
        enemy = gameObject.GetComponent<Enemy>();
        StartCoroutine(rotating());
    }


    private IEnumerator rotating() {


        while (true) {


            if (Globals.player != null && Globals.pause == false && rotateTowardsPlayer == true && (activatedAfterMoveIn == false || (activatedAfterMoveIn == true && enemy.enabled == true))) {


                Vector3 pos = Globals.player.transform.position;


                pos.z = 0;
                Vector2 dir = pos - transform.position;
                float angle = Vector2.SignedAngle(Vector2.right, dir);

                angle = angle + 90;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);


            }


            yield return null;
        }

    }
}
