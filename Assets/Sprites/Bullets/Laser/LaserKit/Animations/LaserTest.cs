using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTest : MonoBehaviour
{
    public List<Animator> Laser;
    public bool Trigger;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       if (Trigger == true){
           foreach(Animator a in Laser){
               a.SetBool("LaserON",true);
           }
       }
       else {
            foreach(Animator a in Laser){
               a.SetBool("LaserON",false);
           }
       }
    }
}
