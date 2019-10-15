using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStatus : MonoBehaviour
{

    private float hp = 100;
    private int Attack;
    private float Deffend=1;
    private float HpPlus;
    private float FirstHP;
    private GameObject equip;
    private GameObject Skill;
    private bool InfinityHealth = false;
    float seconds;

    private void Start()
    {
        Skill = GameObject.Find("FPSController");
        HpPlus = Skill.GetComponent<SkillManagement>().GetHpPlus();
        hp = hp + (hp * HpPlus);
        FirstHP = hp;
     
    }
    public void SetHp(float hp)
    {
        this.hp = hp;
    }

    public float GetHp()
    {
        return hp;
    }

    public float downHp() {
      

            hp = hp - 300f;
        for (int i = 0; i < 100; i++)
        {
            Invoke("CureHp", 1);
        }
        return hp;
    }

    public void CureHp() {
  
            hp += (int)1;
        
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
