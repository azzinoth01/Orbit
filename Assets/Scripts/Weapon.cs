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
        Skill skillObject;
        if (preCreation == false) {
            skillObject = Globals.bulletPool.Find(x => x.gameObject.name == skill.name && x.gameObject.activeSelf == false);
            if (skillObject == null) {
                g = Instantiate(skill, transform.position, transform.rotation);
                g.name = skill.name;
                g.layer = (int)Layer_enum.player_bullets;
                g.GetComponent<Skill>().layerChange();
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(skillObject);
                skillObject.transform.position = transform.position;
                skillObject.transform.rotation = transform.rotation;
                skillObject.gameObject.layer = (int)Layer_enum.player_bullets;
                skillObject.gameObject.SetActive(true);
                g = skillObject.gameObject;
            }

        }
        else {
            g = Instantiate(skill);
            g.name = skill.name;
            g.layer = (int)Layer_enum.player_bullets;



        }



        return g;
    }
}
