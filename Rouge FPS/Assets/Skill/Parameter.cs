using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Parameter : MonoBehaviour
{
    // 仮パラメーター
    public class Param
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
    // CSVDATAの順番
    public enum CSVValue
    {
        NAME = 0,
        VALUE = 1,
        ACTIVE = 2,
        SLOTNUMBER = 3
    };

    public class SkillSlot
    {
        // スキルスロット枠が使えるかどうか(1:ON 0:OFF)
        public int active;
        // スプライトの名前
        public string name;
        // 効果
        public int value;
    }

    // パラメーター実体
    private Param param;



    // スキルスロット最大値
    public const int SLOTMAX = 3;
    // スキルスロット実体
    private SkillSlot[] skillSlot = new SkillSlot[SLOTMAX];
    public SkillSlot[] GetSkillSlot()
    {
        return skillSlot;
    }


    // CSV読み込み用
    private CSV csv;
    // CSVのList
    private List<string[]> csvDatas;

    // 仮描画
    private GameObject text = null;

    // 戻り値がerrの場合、正しい値を受け取っていないです
    public string GetParameter(string _pass)
    {
        // データ名と一致した値を返す
        for(int i = 0; i < csvDatas.Count(); i++)
        {
            if(csvDatas[i][0] == _pass)
            {
                return csvDatas[i][1];
            }
        }
        // エラーチェック
        return "err";
    }

    // パラメーター設定
    // 0リターンで正常、-1リターンでエラーです
    public int SetParameter(string _pass, string _value)
    {
        for(int i = 0; i < csvDatas.Count(); i++)
        {
            if(csvDatas[i][0] == _pass)
            {
                csvDatas[i][1] = _value;
                return 0;
            }
        }
        return -1;
    }
    // Parameter取得
    public Param GetParameter()
    {
        return param;
    }

    // CSVのList取得用
    public List<string[]> GetList()
    {
        return csvDatas;
    }

    void Awake()
    {
        // CSVReaderでデータ読み込み
        csv = new CSV();
        csvDatas = new List<string[]>();
        csv.Read(ref csvDatas, "Read.csv");
        // パラメーター設定
        param = new Param();
        CSVToParam();
    }

    // ParamをCSVData用に変換
    public void ParamToCSV()
    {
        csvDatas[0][1] = param.HP.ToString();
        csvDatas[1][1] = param.ATK.ToString();
        csvDatas[2][1] = param.isATK ? "true" : "false";
        csvDatas[3][1] = param.DEF.ToString();
        csvDatas[4][1] = param.isDEF ? "true" : "false";
        csvDatas[5][1] = param.SPD.ToString();
        csvDatas[6][1] = param.isSPD ? "true" : "false";
        csvDatas[7][1] = param.Magazine.ToString();
        csvDatas[8][1] = param.Ammo.ToString();
        csvDatas[9][1] = param.isPrimary ? "true" : "false";
    }
    // CSVDataをParam用に変換
    public void CSVToParam()
    {
        param.HP = int.Parse(csvDatas[0][1]);
        param.ATK = int.Parse(csvDatas[1][1]);
        param.isATK = csvDatas[2][1] == "true" ? true : false;
        param.DEF = int.Parse(csvDatas[3][1]);
        param.isDEF = csvDatas[4][1] == "true" ? true : false;
        param.SPD = int.Parse(csvDatas[5][1]);
        param.isSPD = csvDatas[6][1] == "true" ? true : false;
        param.Magazine = int.Parse(csvDatas[7][1]);
        param.Ammo = int.Parse(csvDatas[8][1]);
        param.isPrimary = csvDatas[9][1] == "true" ? true : false;
    }

    // csvDataからskillSlotへ変換
    public void CSVToSkillSlot()
    {
        // スキルスロットのカウンター
        var cnt = 0;
        // CSVの縦分回す
        for(var i = 0; i < csvDatas.Count(); i++)
        {
            // CSVデータからアクティブのパラメータをスキルスロットにセット
            if(csvDatas[i][(int)CSVValue.ACTIVE] == "1")
            {
                skillSlot[(int)CSVValue.SLOTNUMBER].name = csvDatas[i][(int)CSVValue.NAME];
                skillSlot[(int)CSVValue.SLOTNUMBER].value = int.Parse(csvDatas[i][(int)CSVValue.VALUE]);
                skillSlot[(int)CSVValue.SLOTNUMBER].active = 1;
                cnt++;
            }
        }
        // 3枠未満だった場合の処理
    }


    // Start is called before the first frame update
    void Start()
    {
        text = new GameObject();
        text = GameObject.Find("CheckParameter");

        // 初期スキル設定
        for (var i = 0; i < 3; i++)
        {
            skillSlot[i].active = 1;
            skillSlot[i].name = "ATK";
            skillSlot[i].value = 10;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // 仮文字表示
        string s = "";
        string tmp = "";
        {
            s += "HP, " + param.HP.ToString() + Environment.NewLine;
            s += "ATK, " + param.ATK.ToString() + Environment.NewLine;
            tmp = param.isATK ? "true" : "false";
            s += "isATK, " + tmp + Environment.NewLine;
            s += "DEF, " + param.DEF.ToString() + Environment.NewLine;
            tmp = param.isDEF ? "true" : "false";
            s += "isDEF, " + tmp + Environment.NewLine;
            s += "SPD, " + param.SPD.ToString() + Environment.NewLine;
            tmp = param.isSPD ? "true" : "false";
            s += "isSPD, " + tmp + Environment.NewLine;
            s += "Magazine, " + param.Magazine.ToString() + Environment.NewLine;
            s += "Ammo, " + param.Ammo.ToString() + Environment.NewLine;
            tmp = param.isPrimary ? "true" : "false";
            s += "isPrimary, " + tmp + Environment.NewLine;
        }
        text.GetComponent<Text>().text = s;
    }
}
