using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_pooling_watcher : MonoBehaviour
{

    public float cleanUpTime;
    public float checkTime;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(cleanUpBullets(checkTime));
    }

    private IEnumerator cleanUpBullets(float wait) {
        yield return new WaitForSeconds(wait);

        float referenzTime = Time.time - cleanUpTime;
        Skill[] recycle = Globals.bulletPool.FindAll(x => x.Timestamp < referenzTime && x.gameObject.activeSelf == false).ToArray();

        foreach (Skill skill in recycle) {
            Globals.bulletPool.Remove(skill);
        }


        foreach (Skill skill in recycle) {
            Destroy(skill.gameObject);
        }

        StartCoroutine(cleanUpBullets(wait));

    }
}
