using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquidHpBar : MonoBehaviour
{
    public GameObject Squid = null;
    private Squid SquidSc = null;

    public GameObject Hpani = null;
    public GameObject HpFrame = null;

    private int AniCnt = 0;

    public GameObject sliderObj = null;
    public Slider slider = null;


    public void Restart()
    {
        Hpani.SetActive(true);
        HpFrame.SetActive(false);
        sliderObj.SetActive(false);

        this.gameObject.SetActive(false);

        AniCnt = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        if(AniCnt < 2)
        {
            Squid = GameObject.Find("Squid");

            SquidSc = Squid.GetComponent<Squid>();

            slider.maxValue = SquidSc.GetHp();
        }
        
        if(AniCnt <= 50)
        {
            AniCnt++;
        }
        else
        {
            Hpani.SetActive(false);
            HpFrame.SetActive(true);
            sliderObj.SetActive(true);

        }




        // HPゲージに値を設定
        slider.value = SquidSc.GetEp().hp;

     

        // Debug.Log(SquidSc.GetEp().hp);
        // Debug.Log(SquidSc.GetHp());

        if(!Squid)
        {
            Restart();
        }


    }
}
