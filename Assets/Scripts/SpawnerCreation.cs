using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCreation : MonoBehaviour
{

    public GameObject refSpawner;

    public Vector2 moveInOutOffset;
    public bool useWaypointOffsetX;
    public bool useWaypointOffsetY;

    public bool flipOffset;


    // Start is called before the first frame update
    void Start() {

        GameObject g = Instantiate(refSpawner, transform);

        Enemy_Spawner spawner = g.GetComponent<Enemy_Spawner>();

        Globals.infityWaveSpawner.Add(g.GetComponent<Enemy_Spawner>());

        Vector2 moveOffset = Vector2.zero;

        if (useWaypointOffsetX == true) {
            moveOffset = new Vector2(transform.position.x - refSpawner.transform.position.x, 0);

        }
        if (useWaypointOffsetY == true) {
            moveOffset = new Vector2(moveOffset.x, transform.position.y - refSpawner.transform.position.y);

        }

        if (flipOffset == true) {
            moveOffset = moveOffset * -1;
        }

        int index = 0;

        for (; index < spawner.modifyWaypoints.Count;) {

            spawner.modifyWaypoints[index] = spawner.modifyWaypoints[index] - moveOffset;

            index = index + 1;
        }

        index = 0;
        for (; index < spawner.modifyMoveIn.Count;) {

            spawner.modifyMoveIn[index] = spawner.modifyMoveIn[index] - moveInOutOffset;

            index = index + 1;
        }
        index = 0;
        for (; index < spawner.modifyMoveOut.Count;) {

            spawner.modifyMoveOut[index] = spawner.modifyMoveOut[index] + moveInOutOffset;

            index = index + 1;
        }

        //g.SetActive(true);

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
