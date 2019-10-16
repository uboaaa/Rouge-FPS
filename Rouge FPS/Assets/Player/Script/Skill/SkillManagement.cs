using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{

    private float[] SkillSpeed = new float[5];
  


    // Start is called before the first frame update
    void Start()
    {
        SkillSpeed[0] = 0.0f;
        SkillSpeed[1] = 0.25f;
        SkillSpeed[2] = 0.5f;
        SkillSpeed[3] = 1.0f;
        SkillSpeed[4] = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {


      

    }

    public float GetAmmoPlus() {
        return SkillSpeed[1];
    }

    public float GetSpeedPlus() {
        return SkillSpeed[4];
    }

    public float GetHpPlus()
    {
        return SkillSpeed[4];}
}
