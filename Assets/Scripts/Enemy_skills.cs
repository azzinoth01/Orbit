using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// verwaltet die skills von enemys
/// </summary>
public class Enemy_skills : MonoBehaviour
{

    public int shootsToCreate;

    public List<Skillsequenze> skillsequenze;
    private GameObject nextSkill;
    private float nextSkillDelay;
    private int skillIndex;

    public int additionalDmg;
    public float dmgModifier;

    private bool isRunning;
    private bool allwoDisable;

    private bool nextSkillRotate;



    /// <summary>
    /// skill gameObjecte im voraus erstellen
    /// </summary>
    private void Awake() {
        nextSkill = skillsequenze[0].Skill;
        nextSkillDelay = skillsequenze[0].Delay;
        nextSkillRotate = skillsequenze[0].ShootInRotatedDirection;
        skillIndex = 0;
        preCreateSkill();
    }

    // Start is called before the first frame update
    void Start() {
        nextSkill = skillsequenze[0].Skill;
        nextSkillDelay = skillsequenze[0].Delay;
        nextSkillRotate = skillsequenze[0].ShootInRotatedDirection;
        skillIndex = 0;
        isRunning = false;
    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {

            if (isRunning == false) {
                isRunning = true;
                StartCoroutine(startSkillTimer(nextSkillDelay));
            }

        }

    }

    /// <summary>
    /// Timer für die activierung der Skills
    /// </summary>
    /// <param name="waitSeconds">delay zeit des Skills in sekunden</param>
    /// <returns></returns>
    private IEnumerator startSkillTimer(float waitSeconds) {

        yield return new WaitForSeconds(waitSeconds);
        activateSkill(false);
        isRunning = false;
    }


    /// <summary>
    /// erzeugt in voraus schon Skill Objecte damit diese nicht zur Laufzeit erstellt werden müssen
    /// </summary>
    private void preCreateSkill() {
        bool needToCreate = false;
        // Debug.Log(Globals.bulletPool.Count);
        foreach (Skillsequenze s in skillsequenze) {
            if (Globals.bulletPool.Count(x => x.gameObject.name == s.Skill.name && x.gameObject.activeSelf == false) < (shootsToCreate / skillsequenze.Count)) {
                needToCreate = true;
                break;
            }
        }


        if (needToCreate == true) {
            for (int i = 0; i < shootsToCreate;) {

                GameObject skill = activateSkill(true);
                skill.SetActive(false);


                i = i + 1;
            }
        }

    }

    /// <summary>
    /// erzeugt Skills und setzt diese auf die Richtige position und activiert diese
    /// prüft vor erzeugung neuer Skills ob diese im bulletpool sind
    /// kann auch Skills im voraus erzeugen, dort wird die Position nicht gesetzt
    /// </summary>
    /// <param name="preCreation">wenn dieser wert True ist, dann werden Skills im voraus erzeugt</param>
    /// <returns> Gameobject vom Skill</returns>
    private GameObject activateSkill(bool preCreation) {
        Skill skill;
        GameObject skillGameObject;
        if (preCreation == false) {
            skill = Globals.bulletPool.Find(x => x.gameObject.name == nextSkill.name && x.gameObject.activeSelf == false);
            if (skill == null) {
                if (nextSkillRotate == true) {
                    skillGameObject = Instantiate(nextSkill, transform.position, transform.rotation);
                }
                else {
                    skillGameObject = Instantiate(nextSkill, transform.position, Quaternion.identity);
                }

                skillGameObject.name = nextSkill.name;
                skillGameObject.layer = (int)Layer_enum.enemy_bullets; // enemy bullet layer ist immer enemy layer -1

                skillGameObject.GetComponent<Skill>().layerChange();
                skillGameObject.GetComponent<Skill>().setDmgModifiers(additionalDmg, dmgModifier);
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(skill);
                skill.transform.position = transform.position;
                if (nextSkillRotate == true) {
                    skill.transform.rotation = transform.rotation;
                }
                else {
                    skill.transform.rotation = Quaternion.identity;
                }


                skill.gameObject.layer = (int)Layer_enum.enemy_bullets;
                skill.setDmgModifiers(additionalDmg, dmgModifier);
                skill.gameObject.SetActive(true);
                skillGameObject = skill.gameObject;

            }

        }
        else {
            skillGameObject = Instantiate(nextSkill);
            skillGameObject.name = nextSkill.name;
            skillGameObject.layer = (int)Layer_enum.enemy_bullets;

        }
        skillIndex = skillIndex + 1;

        if (skillIndex == skillsequenze.Count) {
            skillIndex = 0;
        }

        nextSkill = skillsequenze[skillIndex].Skill;
        nextSkillDelay = skillsequenze[skillIndex].Delay;
        nextSkillRotate = skillsequenze[skillIndex].ShootInRotatedDirection;

        return skillGameObject;
    }

    /// <summary>
    /// checkt ob der enemy über die enemy line gelaufen ist, um die Skills zu aktivieren
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision);
        try {
            if (collision.gameObject.tag == Tag_enum.enemy_border.ToString()) {
                if (allwoDisable == true) {
                    enabled = false;
                }

            }
        }
        catch {

        }
    }

    /// <summary>
    /// funktion die ein disable lock aktiviert, damit die funktion nicht sofort deactiviert wird
    /// </summary>
    private void OnEnable() {
        //Debug.Log("enable");
        allwoDisable = false;

        StartCoroutine(canDisableTimer(1));
    }

    private void OnDisable() {

    }

    /// <summary>
    /// disable allow timer
    /// </summary>
    /// <param name="wait"> delaytimer in Sekunden</param>
    /// <returns></returns>
    private IEnumerator canDisableTimer(float wait) {
        yield return new WaitForSeconds(wait);
        allwoDisable = true;
    }

}
