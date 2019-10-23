using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Param : MonoBehaviour
{
    // 仮パラメーター
    public class Parameter
    {
        public int HP;
        public int ATK;
        public bool isATK;
        public int DEF;
        public bool isDEF;
        public int SPD;
        public bool isSPD;
        public int Magazine;
        public int Ammo;
        public bool isPrimary;
    }
    public static Parameter param;
    
    GameObject text;
    CSVReader csvr;

    // Parameter取得
    public static Parameter GetParameter()
    {
        return param;
    }

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
}
