using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    private float LookHP = 0;
    public Text text;
    
     GameObject FPSCon;
    // Start is called before the first frame update
    void Start()
    {
    FPSCon = GameObject.Find("FPSController");
     LookHP= FPSCon.GetComponent<MyStatus>().GetHp();
    }

    // Update is called once per frame
    void Update()
    {
        LookHP = FPSCon.GetComponent<MyStatus>().GetHp();
        if (LookHP > 0) { text.text = LookHP + ""; }
        else { text.text = "0"; }
    }
}
