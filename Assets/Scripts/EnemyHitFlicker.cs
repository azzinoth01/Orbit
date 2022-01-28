using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitFlicker : MonoBehaviour
{

    private int frameCounter;

    private Color hitColor;
    private Color baseColor;
    private float diffR;
    private float diffG;
    private float diffB;

    private SpriteRenderer spriteRenderer;

    private int hitFlickerInQue;

    public int HitFlickerInQue {
        get {
            return hitFlickerInQue;
        }

        set {
            hitFlickerInQue = value;
        }
    }


    // Start is called before the first frame update
    void Start() {

        hitColor = new Color(1, 0.25f, 0.25f, 1);

        baseColor = gameObject.GetComponent<SpriteRenderer>().color;

        diffR = (hitColor.r - baseColor.r) / 60;
        diffG = (hitColor.g - baseColor.g) / 60;
        diffB = (hitColor.b - baseColor.b) / 60;




        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        frameCounter = 0;

        StartCoroutine(hitflicker());
    }




    private IEnumerator hitflicker() {




        while (true) {



            if (hitFlickerInQue > 0 && Globals.pause == false) {

                if (frameCounter == 0) {
                    spriteRenderer.color = hitColor;
                }
                else {
                    spriteRenderer.color = new Color(spriteRenderer.color.r - diffR, spriteRenderer.color.g - diffG, spriteRenderer.color.b - diffB);
                }

                frameCounter = frameCounter + 1;
                if (frameCounter == 60) {
                    frameCounter = 0;

                    spriteRenderer.color = baseColor;

                    hitFlickerInQue = hitFlickerInQue - 1;
                }
            }




            yield return null;
        }


    }
}