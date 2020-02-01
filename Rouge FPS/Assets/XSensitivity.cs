using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class XSensitivity : MonoBehaviour
{
    // Start is called before the first frame update
   
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
      if(transform.name=="+"){
      Up=true;}
      if(transform.name=="-"){
      Down=true;}
      }

    // Update is called once per frame


    
 
}
