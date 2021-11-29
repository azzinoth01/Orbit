using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject skill;
    public float reloadTime;
    private bool canShoot;

    public int shootsToCreate;
    public Animator anim;

    public int additionalDmg;
    public float dmgModifier;
    // Start is called before the first frame update
    void Start() {

    }
    private void OnEnable() {
        canShoot = true;
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

            // damit ich momentan keine Fehlermeldungen kriege
            try {
                anim.SetFloat("Angle", transform.eulerAngles.z);
            }
            catch {

            }

        }
    }

    private IEnumerator shootTimer(float wait) {

        yield return new WaitForSeconds(wait);
        canShoot = true;
    }

    public void shoot(int additionalDmg, float dmgModifier) {
        if (canShoot == true) {
            canShoot = false;

            GameObject g = activateSkill(false);
            g.GetComponent<Skill>().setDmgModifiers(additionalDmg + this.additionalDmg, dmgModifier * this.dmgModifier);

            StartCoroutine(shootTimer(reloadTime));
        }
    }

    public GameObject activateSkill(bool preCreation) {
        GameObject g;
        if (preCreation == false) {
            g = Globals.bulletPool.Find(x => x.name == skill.name && x.activeSelf == false);
            if (g == null) {
                g = Instantiate(skill, transform.position, transform.rotation);
                g.name = skill.name;
                g.layer = 6; // player bullet layer ist layer 6
                g.GetComponent<Skill>().layerChange();
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(g);
                g.transform.position = transform.position;
                g.transform.rotation = transform.rotation;
                g.layer = 6;
                g.SetActive(true);
            }

        }
        else {
            g = Instantiate(skill);
            g.name = skill.name;
            g.layer = 6;



        }



        return g;
    }
}
