using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    private void Awake() {
        nextSkill = skillsequenze[0].Skill;
        nextSkillDelay = skillsequenze[0].Delay;
        skillIndex = 0;
        preCreateSkill();
    }

    // Start is called before the first frame update
    void Start() {
        nextSkill = skillsequenze[0].Skill;
        nextSkillDelay = skillsequenze[0].Delay;
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

    private IEnumerator startSkillTimer(float waitSeconds) {

        yield return new WaitForSeconds(waitSeconds);
        activateSkill(false);
        isRunning = false;
    }


    private void preCreateSkill() {
        for (int i = 0; i < shootsToCreate;) {

            GameObject skill = activateSkill(true);
            skill.SetActive(false);


            i = i + 1;
        }
    }

    private GameObject activateSkill(bool preCreation) {
        GameObject skill;
        if (preCreation == false) {
            skill = Globals.bulletPool.Find(x => x.name == nextSkill.name && x.activeSelf == false);
            if (skill == null) {
                skill = Instantiate(nextSkill, transform.position, Quaternion.identity);
                skill.name = nextSkill.name;
                skill.layer = gameObject.layer - 1; // enemy bullet layer ist immer enemy layer -1
                skill.GetComponent<Skill>().layerChange();
                skill.GetComponent<Skill>().setDmgModifiers(additionalDmg, dmgModifier);
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(skill);
                skill.transform.position = transform.position;
                skill.transform.rotation = Quaternion.identity;
                skill.layer = gameObject.layer - 1;
                skill.GetComponent<Skill>().setDmgModifiers(additionalDmg, dmgModifier);
                skill.SetActive(true);


            }

        }
        else {
            skill = Instantiate(nextSkill);
            skill.name = nextSkill.name;
            skill.layer = gameObject.layer - 1;

        }
        skillIndex = skillIndex + 1;

        if (skillIndex == skillsequenze.Count) {
            skillIndex = 0;
        }

        nextSkill = skillsequenze[skillIndex].Skill;
        nextSkillDelay = skillsequenze[skillIndex].Delay;

        return skill;
    }

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
    private void OnEnable() {
        //Debug.Log("enable");
        allwoDisable = false;

        StartCoroutine(canDisableTimer(1));
    }

    private IEnumerator canDisableTimer(float wait) {
        yield return new WaitForSeconds(wait);
        allwoDisable = true;
    }

}