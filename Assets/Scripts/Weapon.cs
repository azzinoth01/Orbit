using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject skill;
    public float reloadTime;
    private bool canShoot;
    private float time;
    public int shootsToCreate;
    public Animator anim;
    // Start is called before the first frame update
    void Start() {
        time = 0;
    }
    private void Awake() {
        for (int i = 0; i < shootsToCreate;) {
            GameObject g = activateSkill(true);
            g.SetActive(false);
            i = i + 1;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            if (time >= reloadTime) {
                canShoot = true;
                time = 0;
            }
            else if (canShoot == false) {
                time = time + Time.deltaTime;
            }
            // damit ich momentan keine Fehlermeldungen kriege
            try {
                anim.SetFloat("Angle", transform.eulerAngles.z);
            }
            catch {

            }

        }
    }

    public void shoot() {
        if (canShoot == true) {
            canShoot = false;
            activateSkill(false);
        }
    }

    public GameObject activateSkill(bool preCreation) {
        GameObject g;
        if (preCreation == false) {
            g = Globals.bulletPool.Find(x => x.name == skill.name && x.activeSelf == false);
            if (g == null) {
                g = Instantiate(skill, transform.position, transform.rotation);
                g.name = skill.name;
                g.layer = gameObject.layer - 1; // player bullet layer ist immer player layer -1
                g.GetComponent<Skill>().layerChange();
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(g);
                g.transform.position = transform.position;
                g.transform.rotation = transform.rotation;
                g.layer = gameObject.layer - 1;
                g.SetActive(true);
            }

        }
        else {
            g = Instantiate(skill);
            g.name = skill.name;
            g.layer = gameObject.layer - 1;



        }



        return g;
    }
}
