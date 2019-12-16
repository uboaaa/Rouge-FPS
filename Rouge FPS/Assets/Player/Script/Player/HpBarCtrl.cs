using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class HpBarCtrl : MonoBehaviour
{
    float hp;
    float firsthp;
    Slider _slider;
    GameObject gameObject;
    void Start()
    {
        gameObject = GameObject.Find("FPSController");
        // 値セット
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
         hp = gameObject.GetComponent<MyStatus>().GetHp();
        firsthp = gameObject.GetComponent<MyStatus>().GetHp();
        _slider.maxValue = gameObject.GetComponent<MyStatus>().GetHp();
    }

  
    void Update()
    {

        hp = gameObject.GetComponent<MyStatus>().GetHp();
        //後で消す
        //GameObject.Find("FPSController").GetComponent<MyStatus>().downHp();
        if (hp > firsthp)
        {
            // 最大を超えたら0に戻す
            //hp = 0;
        }

        // HPゲージに値を設定
        _slider.value = hp;
    }
}
