using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class HpBarCtrl : MonoBehaviour
{
    int hp;
    int firsthp;
    Slider _slider;
    void Start()
    {
        // 値セット
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
         hp = GameObject.Find("FPSController").GetComponent<MyStatus>().GetHp();
        firsthp = GameObject.Find("FPSController").GetComponent<MyStatus>().GetHp();
        _slider.maxValue = GameObject.Find("FPSController").GetComponent<MyStatus>().GetHp();
    }

  
    void Update()
    {

        hp = GameObject.Find("FPSController").GetComponent<MyStatus>().GetHp();
        //後で消す
        //GameObject.Find("FPSController").GetComponent<MyStatus>().downHp();
        if (hp > 100)
        {
            // 最大を超えたら0に戻す
            hp = 0;
        }

        // HPゲージに値を設定
        _slider.value = hp;
    }
}
