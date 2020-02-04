using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameter : MonoBehaviour
{
    // プレイヤーのパラメーター
    static public int HP;
    static public int ATK;
    static public bool IsATK;
    static public int DEF;
    static public bool IsDEF;
    static public int SPD;
    static public bool IsSPD;
    static public int Magazine;
    static public int Ammo;
    static public bool IsPrimary;

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        // 数値初期化
        HP = 0;
        ATK = 0;
        IsATK = false;
        DEF = 0;
        IsDEF = false;
        SPD = 0;
        IsSPD = false;
        Magazine = 0;
        Ammo = 0;
        IsPrimary = false;
    }

    // 現在のスキルスロット名
    public string[] slotName;
    // スロット名取得
    public string GetSlotName(int _num)
    {
        return slotName[_num];
    }
    public string[] GetAllSlotName()
    {
        return slotName;
    }
    // スロット名設定
    public void SetSlotName(string _name, int _num)
    {
        slotName[_num] = _name;
    }
    // 重複スキル設定時の値を分けるための変数
    public string[] slotValue;
    public string GetSlotValue(int _num)
    {
        return slotValue[_num];
    }

    // パラメーター取得
    static public object GetParameter(string _name)
    {
        switch (_name)
        {
            case "HP":
                return HP;
            case "ATK":
                return ATK;
            case "DEF":
                return DEF;
            case "SPD":
                return SPD;
            case "Magazine":
                return Magazine;
            case "Ammo":
                return Ammo;
            case "IsATK":
                return IsATK;
            case "IsDEF":
                return IsDEF;
            case "IsSPD":
                return IsSPD;
            case "IsPrimary":
                return IsPrimary;
        }
        return "err";
    }
    // 引数_nameと同じ変数名の値をstringで返す
    public string GetParameterToString(string _name)
    {
        switch (_name)
        {
            case "HP":
                return HP.ToString();
            case "ATK":
                return ATK.ToString();
            case "DEF":
                return DEF.ToString();
            case "SPD":
                return SPD.ToString();
            case "Magazine":
                return Magazine.ToString();
            case "Ammo":
                return Ammo.ToString();
            case "IsATK":
                return IsATK ? "true" : "false";
            case "IsDEF":
                return IsDEF ? "true" : "false";
            case "IsSPD":
                return IsSPD ? "true" : "false";
            case "IsPrimary":
                return IsPrimary ? "true" : "false";
        }
        return "err";
    }
    // パラメーター設定
    // _num番目のスロットの_nameと同じ変数名(パラメーター)に_valueを入れる
    public void SetParameter(string _name, object _value, int _num)
    {
        switch (_value)
        {
            case int n when _name == "HP":
                HP += (int)_value;
                slotValue[_num] = _value.ToString();
                break;
            case int n when _name == "ATK":
                ATK += (int)_value;
                slotValue[_num] = _value.ToString();
                break;
            case int n when _name == "DEF":
                DEF += (int)_value;
                slotValue[_num] = _value.ToString();
                break;
            case int n when _name == "SPD":
                SPD += (int)_value;
                slotValue[_num] = _value.ToString();
                break;
            case int n when _name == "Magazine":
                Magazine += (int)_value;
                slotValue[_num] = _value.ToString();
                break;
            case int n when _name == "Ammo":
                Ammo += (int)_value;
                slotValue[_num] = _value.ToString();
                break;
            case bool b when _name == "IsATK":
                IsATK = (bool)_value;
                slotValue[_num] = _value.ToString();
                break;
            case bool b when _name == "IsDEF":
                IsDEF = (bool)_value;
                slotValue[_num] = _value.ToString();
                break;
            case bool b when _name == "IsSPD":
                IsSPD = (bool)_value;
                slotValue[_num] = _value.ToString();
                break;
            case bool b when _name == "IsPrimary":
                IsPrimary = (bool)_value;
                slotValue[_num] = _value.ToString();
                break;
        }
    }
    // パラメーター全リセット
    public void AllReset()
    {
        HP = 0;
        ATK = 0;
        IsATK = false;
        DEF = 0;
        IsDEF = false;
        SPD = 0;
        IsSPD = false;
        Magazine = 0;
        Ammo = 0;
        IsPrimary = false;
        for (var i = 0; i < 3; i++) 
        {
            slotName[i] = "none";
        }
    }
    
}
