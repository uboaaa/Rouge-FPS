using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
  private MyStatus test;
  private FirstPersonGunController controller;
  private float FirstHP;
  private   float NowHP;
  GameObject game;
  private  Image img;
  private bool once = false;
    void Start()
    {
        game = GameObject.Find("FPSController");
        img = GetComponent<Image>();
        img.color = Color.clear;
        test = GetComponent<MyStatus>();
    
        
    }

    void Update()
    {

        if (!once) {
            FirstHP = game.GetComponent<MyStatus>().GetHp();
            NowHP = game.GetComponent<MyStatus>().GetHp();
            once = true;
        }
     //HPを持ってくる
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("FirstHp" + FirstHP);
            NowHP = game.GetComponent<MyStatus>().downHp();
            Debug.Log("NowHp" + NowHP);

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