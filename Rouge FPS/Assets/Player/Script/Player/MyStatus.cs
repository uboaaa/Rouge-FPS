using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStatus : MonoBehaviour
{

   private int hp=100;
    private int Attack;
    private int Deffend;
    private GameObject equip;
    private GameObject Skill;
    private bool InfinityHealth = false;

    private void Start()
    {
        Skill = GameObject.Find("FPSController");
    }
    public void SetHp(int hp)
    {
        this.hp = hp;
    }

    public int GetHp()
    {


        return hp;
    }

    public int downHp() {
        InfinityHealth = Skill.GetComponent<SkillManagement>().GetHealthFlg();
        if (!InfinityHealth)
        {
            hp = hp - 9;
            return hp;
        }
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
