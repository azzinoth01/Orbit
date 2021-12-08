using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �berpr�ft die bulletpooling liste
/// </summary>
public class Bullet_pooling_watcher : MonoBehaviour
{

    public float cleanUpTime;
    public float checkTime;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(cleanUpBullets(checkTime));
    }

    /// <summary>
    /// �berpr�ft die bulletpooling liste ob ein Skill l�nger als cleanUpTime inactive war
    /// </summary>
    /// <param name="wait">der intervall in Sekunden bis zum n�chsten check</param>
    /// <returns></returns>
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
