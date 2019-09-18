using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    private int LookHP = 0;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
     LookHP= GameObject.Find("FPSController").GetComponent<MyStatus>().GetHp();
    }

    // Update is called once per frame
    void Update()
    {
        LookHP = GameObject.Find("FPSController").GetComponent<MyStatus>().GetHp();
        if (LookHP > 0) { text.text = LookHP + ""; }
        else { text.text = "0"; }
    }
}
