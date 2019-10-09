using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStatus : MonoBehaviour
{

   private float hp=100;
    private int Attack;
    private int Deffend;
    private float HpPlus;
    private GameObject equip;
    private GameObject Skill;
    private bool InfinityHealth = false;

    private void Start()
    {
        Skill = GameObject.Find("FPSController");
        HpPlus = Skill.GetComponent<SkillManagement>().GetHpPlus();
        hp = hp + (hp * HpPlus);
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
     
       
            hp = hp - 9;

        return hp;
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
