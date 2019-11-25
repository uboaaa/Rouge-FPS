using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{

    private static float[] SkillSpeed = new float[5];
    private static int AmmoMagnification;
    private static int HPMagnification;
    private static int SpeedMagnification;

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


      

    }

    public static float GetAmmoPlus() {
        return SkillSpeed[AmmoMagnification];
    }

    public static float GetSpeedPlus() {
        return SkillSpeed[SpeedMagnification];
    }

    public static float GetHpPlus()
    {
        return SkillSpeed[HPMagnification];}


    public static void SetAmmoMagnification(int SetNumber){
       AmmoMagnification=SetNumber;
    }    

    public static void SetHPMagnification(int SetNumber){
       HPMagnification=SetNumber;
    }    
    public static void SetSpeedMagnification(int SetNumber){
       SpeedMagnification=SetNumber;
    }    
}
