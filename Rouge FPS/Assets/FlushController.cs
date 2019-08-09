using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
    Image img;

    MyStatus test;
    int FirstHP;
    int NowHP;
    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;
        test = GetComponent<MyStatus>();
        FirstHP = test.GetHp();
    }

    void Update()
    {

        Debug.Log(FirstHP);
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log(NowHP);
        }

        if (FirstHP>NowHP)
        {

                this.img.color = new Color(0.5f, 0f, 0f, 0.5f);
            
        }
        else
        {
            this.img.color = Color.Lerp(this.img.color, Color.clear, Time.deltaTime);
        }

    }
}