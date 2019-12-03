using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{

    private static float[] SkillSpeed = new float[5];
    private static int AmmoMagnification;
    private static int HPMagnification;
    
    private static int SpeedMagnification;
    

    private static bool BulletScaleUp;
    

    private static bool The_World;
    
     private void Awake() {SetBulletScaleUp(true);}
    // Start is called before the first frame update
    void Start()
    {
        SkillSpeed[0] = 0.0f;
        SkillSpeed[1] = 0.25f;
        SkillSpeed[2] = 0.5f;
        SkillSpeed[3] = 1.0f;
        SkillSpeed[4] = 5.0f;
        AmmoMagnification=0;
        HPMagnification=0;
        SpeedMagnification=0;
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetMouseButtonDown(1) && !The_World){The_World=true;}
    if(The_World){Invoke("SetTimeStop",5);Debug.Log("test");}
    }

    private  void SetTimeStop(){The_World=false;}
    public static float GetAmmoPlus() {return SkillSpeed[AmmoMagnification];}

    public static float GetSpeedPlus() {return SkillSpeed[SpeedMagnification];}

    public static float GetHpPlus(){return SkillSpeed[HPMagnification];}

     public static bool GetBulletScaleUp(){return BulletScaleUp;}

     public static bool GetTimeStop(){return The_World;}


    public static void SetAmmoMagnification(int SetNumber){AmmoMagnification=SetNumber;}    
    
     public static void SetBulletScaleUp(bool SetBool){BulletScaleUp=SetBool;}  
    public static void SetHPMagnification(int SetNumber){HPMagnification=SetNumber;}    
    public static void SetSpeedMagnification(int SetNumber){SpeedMagnification=SetNumber;}    
}
