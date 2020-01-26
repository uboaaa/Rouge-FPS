using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    private static float XRotation;
    void Start()
    {
   
        XRotation=PlayerPrefs.GetFloat("XRotate",5.0f);
    }

    // Update is called once per frame
    void Update()
    {
    if(XSensitivity.GetUpPool()){XRotation+=1.0f;XSensitivity.FalseUpPool();}
    if(XSensitivity.GetDownPool()){XRotation-=1.0f;XSensitivity.FalseDownPool();}
   
        
    }
     public static float GetX(){return XRotation;}

     public static void SetX(float aaa){XRotation=aaa;}
        public static void Save(){ 
        PlayerPrefs.SetFloat ("XRotate", XRotation);
        PlayerPrefs.Save ();}
}
