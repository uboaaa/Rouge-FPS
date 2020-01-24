using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{

    private GameObject player = null;

    public Slider slider = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");

        slider.maxValue = player.GetComponent<MyStatus>().GetHp();
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<MyStatus>().GetHp();
    }
}
