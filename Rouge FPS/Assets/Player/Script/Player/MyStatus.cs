using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStatus : MonoBehaviour
{

    public static float playerHP = 100;
    private int playerATK;
    private float playerDEF = 1;
    private float HpPlus;
    private float FirstHP;
    private GameObject equip;
    private GameObject Skill;
    private bool InfinityHealth = false;
    float seconds;

    private void Start()
    {
        Skill = GameObject.Find("FPSController");
        HpPlus = Skill.GetComponent<SkillManagement>().GetHpPlus(0);
        playerHP = playerHP + (playerHP * HpPlus);
        FirstHP = playerHP;
     
    }
    public void SetHp(float hp)
    {
        playerHP = hp;
    }

    public static float GetHp()
    {
        return playerHP;
    }

    public static void downHp(float damage) {
      

            playerHP = playerHP - damage;
     

    }

    public void CureHp() {
  
            playerHP += (int)1;
        
    }

    
    public void SetEquip(GameObject weapon)
    {
        equip = weapon;
    }

    public GameObject GetEquip()
    {
        return equip;
    }
}
