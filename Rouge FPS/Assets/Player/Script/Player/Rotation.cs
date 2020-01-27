using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    private static float XRotation;
    private static int Count;
    void Start()
    { PlayerPrefs.SetFloat ("XRotate", 2.0f);
    PlayerPrefs.Save ();
        Count=PlayerPrefs.GetInt("Count",3);
        XRotation=PlayerPrefs.GetFloat("XRotate",2.0f);
    }

    // Update is called once per frame
    void Update()
    {
    if(XSensitivity.GetUpPool() &&Count<14){XRotation+=0.2f;Count++;XSensitivity.FalseUpPool();}
    if(XSensitivity.GetDownPool() && Count>1){XRotation-=0.2f;Count--;XSensitivity.FalseDownPool();}
   
        
    }
     public static float GetX(){return XRotation;}

     public static int GetCount(){return Count;}

     public static void SetX(float aaa){XRotation=aaa;}
        public static void Save(){ 
        PlayerPrefs.SetFloat ("XRotate", XRotation);
        PlayerPrefs.GetInt("Count",Count);
        PlayerPrefs.Save ();
        }
}
