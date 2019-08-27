using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyStatus : MonoBehaviour
{

   public int hp=100;
    private int Attack;
    private int Deffend;
    private GameObject equip;
    public Text HpShow;

    void Start()
    {

    }

    void Update()
    {
        HpShow.text = "HP "+hp;
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
        hp = hp - 1 ;
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
