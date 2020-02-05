using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStatus : MonoBehaviour
{

    private float playerHP = 100;
    private int playerATK;
    private float playerDEF = 1;
    private float HpPlus;
    private static float FirstHP;
    private GameObject equip;
    private GameObject Skill;
    private bool InfinityHealth = false;
    float seconds;

    private void Start()
    {
        Skill = GameObject.Find("FPSController");
        HpPlus = SkillManagement.GetHpPlus();
        playerHP = 100+PlayerParameter.HP;
        FirstHP = playerHP;
     
    }
    public void SetHp(float hp)
    {
        this.playerHP = hp;
    }

    public float GetHp()
    {
        return playerHP;
    }
    public void AddHp(float hp)
    {
        // AudioManager.Instance.PlaySE("heal01"); 
        if((this.playerHP += hp) >FirstHP){this.playerHP=FirstHP;}
        if((this.playerHP += hp) <FirstHP){this.playerHP += hp;}
    }

    public float GetMaxHp()
    {
        return FirstHP;
    }

    public static void NewMaxHP(){
        FirstHP=0;
        FirstHP=100+PlayerParameter.HP;}


    public void downHp(float damage) {
      

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
