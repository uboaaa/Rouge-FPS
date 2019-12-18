using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
  private MyStatus test;
  private float FirstHP;
  private   float NowHP;
  GameObject FPSCon;
  private  Image img;
  private bool once = false;
    void Start()
    {
        
     FPSCon = GameObject.Find("FPSController");
        img = GetComponent<Image>();
        img.color = Color.clear;
        test = GetComponent<MyStatus>();
    
        
    }

    void Update()
    {
        // Debug.Log(NowHP);

        if (!once) {
            FirstHP = FPSCon.GetComponent<MyStatus>().GetHp();
            NowHP = FPSCon.GetComponent<MyStatus>().GetHp();
            once = true;
        }
     //HPを持ってくる
        if (EnemyAttackPower.GetEnemyBHitGet())
        {
            // Debug.Log("FirstHp" + FirstHP);
          
            NowHP = FPSCon.GetComponent<MyStatus>().GetHp();
            // Debug.Log("NowHp" + NowHP);

        }

        //ここのif文に当たった処理を！
        if (EnemyAttackPower.GetEnemyBHitGet())
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