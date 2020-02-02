using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlushController : MonoBehaviour
{
  private MyStatus test;
  private float FirstHP;
  private   float NowHP;
  GameObject FPSCon;
  private  Image img;
  private bool once = false;
  private float alpha;
   private float red;
  float fadeSpeed = 0.01f; //透明度が変わるスピードを管理
   Image fadeImage;        //透明度を変更するパネルのイメージ


    void Start()
    {
        
     FPSCon = GameObject.Find("FPSController");
        img = GetComponent<Image>();
        img.color = Color.clear;
        test = GetComponent<MyStatus>();
        fadeImage = GetComponent<Image>();
  
    
        
    }

    void Update()
    {
        // Debug.Log(EnemyAttackPower.GetEnemyBHitGet());

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
       if(NowHP<=0){GameOverEnter();
       if(alpha>=1){Invoke("GameOver",1.0f);}
       }
       
        //ここのif文に当たった処理を！
        if(NowHP>0){
        if (EnemyAttackPower.GetEnemyBHitGet() && this.img.color.r<0.1f)
        {
            AudioManager.Instance.PlaySE("damage1");
            this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
  
            FirstHP = NowHP;

        }
        else
        {
            this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
        }
     }

    }
   void GameOver(){AudioManager.Instance.StopBGM(); SceneManager.LoadScene("GameOverScene");}
    void GameOverEnter(){
            alpha += fadeSpeed; 
            red+=fadeSpeed;
           SetAlpha();

    }
   public float GetRed(){return this.img.color.r;}
    void SetAlpha()
    {
        fadeImage.color = new Color(red, 0.0f, 0.0f, alpha);
    }
}