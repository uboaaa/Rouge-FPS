using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyStatus : MonoBehaviour
{

    private int hp;
    private GameObject equip;

    public void SetHp(int hp)
    {
        this.hp = hp;
    }

    public int GetHp()
    {
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
