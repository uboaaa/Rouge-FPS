using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
  MyStatus test;
    FirstPersonGunController controller;
    int FirstHP;
    int NowHP;
    Image img;
    void Start()
    {
       
        img = GetComponent<Image>();
        img.color = Color.clear;
        test = GetComponent<MyStatus>();
        FirstHP = GameObject.Find("FirstPersonCharacter").GetComponent<MyStatus>().GetHp();
        NowHP = GameObject.Find("FirstPersonCharacter").GetComponent<MyStatus>().GetHp();
        
    }

    void Update()
    {
     //HPを持ってくる
        if (Input.GetMouseButtonDown(0))
        {
            //NowHP = GameObject.Find("FirstPersonCharacter").GetComponent<MyStatus>().downHp();
            //Debug.Log(FirstHP);
            //Debug.Log(NowHP);
        }

        //ここのif文に当たった処理を！
        if (FirstHP>NowHP)
        {
            this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
            FirstHP = NowHP;
        }
        else
        {
            this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
        }

    }
}