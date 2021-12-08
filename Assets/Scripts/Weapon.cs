using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// beschreibt die Waffen des Players
/// </summary>
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
    /// <summary>
    /// waffe kann standardmäßig von anfang an schon schießen
    /// </summary>
    private void OnEnable() {
        canShoot = true;
    }

    /// <summary>
    /// erzeugt im voraus Skill Objecte damit diese nicht zur laufzeit erzeugt werden müssen
    /// </summary>
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

    /// <summary>
    /// delay Timer zwischen den schüssen
    /// </summary>
    /// <param name="wait"> zeit zwischen den schüssen in sekunden</param>
    /// <returns></returns>
    private IEnumerator shootTimer(float wait) {

        yield return new WaitForSeconds(wait);
        canShoot = true;
    }

    /// <summary>
    /// Erzeugung der Skill objecten mit dmg Modifiern, wenn die Waffe schießen kann
    /// </summary>
    /// <param name="additionalDmg">erhöhten den schaden der bullet direkt über diesen Wert</param>
    /// <param name="dmgModifier">nach hinzufügen des additionalDmg modifiers wir der dmg mit diesem Wert multipliziert</param>
    public void shoot(int additionalDmg, float dmgModifier) {
        if (canShoot == true) {
            canShoot = false;

            GameObject g = activateSkill(false);
            g.GetComponent<Skill>().setDmgModifiers(additionalDmg + this.additionalDmg, dmgModifier * this.dmgModifier);

            StartCoroutine(shootTimer(reloadTime));
        }
    }

    /// <summary>
    ///  erzeugt Skills und setzt diese auf die Richtige position und activiert diese
    /// prüft vor erzeugung neuer Skills ob diese im bulletpool sind
    /// kann auch Skills im voraus erzeugen, dort wird die Position nicht gesetzt
    /// </summary>
    /// <param name="preCreation">wenn dieser wert True ist, dann werden Skills im voraus erzeugt</param>
    /// <returns>Gameobject vom Skill</returns>
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
