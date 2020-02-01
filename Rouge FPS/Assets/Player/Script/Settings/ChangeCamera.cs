using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
   private static bool Up=false;
   private static bool Down=false;

    private void Awake() {
   
    }
    void Start()
    {
    Up=false;
    Down=false;
    }
    public static bool GetUpPool(){return Up;}

    public static void FalseUpPool(){Up=false;}

    
    public static bool GetDownPool(){return Down;}

    public static void FalseDownPool(){Down=false;}
     public void Onclick(){
      if(transform.name=="C+"){
      Up=true;}
      if(transform.name=="C-"){
      Down=true;}
      }

    // Update is called once per frame


    
}
