using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// =========================
// パラメータクラス
//　
// ここの数値を変化させる
// 
// =========================
public class Parameter : MonoBehaviour
{
    // スキルステータス
    public class Status
    { 
        public int HP { get; set; } = 0;
        public int ATK { get; set; } = 0;
        public bool IsATK { get; set; } = false;
        public int DEF { get; set; } = 0;
        public bool IsDEF { get; set; } = false;
        public int SPD { get; set; } = 0;
        public bool IsSPD { get; set; } = false;
        public int Magazine { get; set; } = 0;
        public int Ammo { get; set; } = 0;
        public bool IsPrimary { get; set; } = false;
    };
    public Status status = new Status();
    // スキルスロットの内容
    public class SkillSlot
    {
        // スプライトの名前
        public string name = "";
        // 効果
        public int value = 0;
        // スキルスロット枠が使えるかどうか(1:ON 0:OFF)
        public int active = 0;
    }
    // スキルスロット最大値
    public const int SLOTMAX = 3;
    // スキルスロット実体
    private SkillSlot[] nowSlot = new SkillSlot[SLOTMAX];
    public SkillSlot[] GetNowSlot()
    {
        return nowSlot;
    }
    // 抽選されたスキル
    private SkillSlot[] newSlot = new SkillSlot[SLOTMAX];
    public SkillSlot[] GetNewSlot()
    {
        return newSlot;
    }
    public void SetNewSlot(string _name, int _value, int _slotNum)
    {
        newSlot[_slotNum].name = _name;
        newSlot[_slotNum].value = _value;
        newSlot[_slotNum].active = 1;
    }
    // CSV読み込み用
    private CSV csv = new CSV();
    // CSVのList
    private List<string[]> csvDatas;
    // 仮描画
    private GameObject text;
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

    // CSVのList取得用
    public List<string[]> GetList()
    {
        return csvDatas;
    }

    void Awake()
    {
        // CSVReaderでデータ読み込み
        csvDatas = new List<string[]>();
        csv.Read(ref csvDatas, "Read.csv");
        // CSV→Statusに変更
        CSVToStatus();
        for(var i = 0; i< SLOTMAX; i++)
        {
            nowSlot[i] = new SkillSlot();
        }
        CSVToSkillSlot();
    }

    // StatusをCSVData用に変換
    public void StatusToCSV()
    {
        csvDatas[0][1] = status.HP.ToString();
        csvDatas[1][1] = status.ATK.ToString();
        csvDatas[2][1] = status.IsATK ? "true" : "false";
        csvDatas[3][1] = status.DEF.ToString();
        csvDatas[4][1] = status.IsDEF ? "true" : "false";
        csvDatas[5][1] = status.SPD.ToString();
        csvDatas[6][1] = status.IsSPD ? "true" : "false";
        csvDatas[7][1] = status.Magazine.ToString();
        csvDatas[8][1] = status.Ammo.ToString();
        csvDatas[9][1] = status.IsPrimary ? "true" : "false";
    }
    // CSVDataをStatus用に変換
    public void CSVToStatus()
    {
        
        status.HP = int.Parse(csvDatas[0][1]);
        status.ATK = int.Parse(csvDatas[1][1]);
        status.IsATK = csvDatas[2][1] == "true" ? true : false;
        status.DEF = int.Parse(csvDatas[3][1]);
        status.IsDEF = csvDatas[4][1] == "true" ? true : false;
        status.SPD = int.Parse(csvDatas[5][1]);
        status.IsSPD = csvDatas[6][1] == "true" ? true : false;
        status.Magazine = int.Parse(csvDatas[7][1]);
        status.Ammo = int.Parse(csvDatas[8][1]);
        status.IsPrimary = csvDatas[9][1] == "true" ? true : false;
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
            if (csvDatas[i][(int)SkillStatus.CSVParameter.ACTIVE] == "1")
            {
                // スロット番号を取得
                var num = int.Parse(csvDatas[i][(int)SkillStatus.CSVParameter.SLOTNUMBER]);
                // そのスロット番号に格納
                nowSlot[num].name = csvDatas[i][(int)SkillStatus.CSVParameter.NAME];
                nowSlot[num].value = int.Parse(csvDatas[i][(int)SkillStatus.CSVParameter.VALUE]);
                nowSlot[num].active = 1;
                cnt++;
            }
        }
        // 3枠未満だった場合の処理
        if(cnt < 3)
        {
            // noneを入れる
            for (var i = cnt; i < SLOTMAX; i++)
            {
                nowSlot[i].name = "none";
                nowSlot[i].value = 0;
                nowSlot[i].active = 0;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("CheckParameter");
    }
    // Update is called once per frame
    void Update()
    {
        // 仮文字表示
        string s = "";
        string tmp = "";
        /*
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
        */
        //text.GetComponent<Text>().text = s;
    }
}
