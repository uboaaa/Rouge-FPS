using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameter : MonoBehaviour
{
    // プレイヤーのパラメーター
    public int HP;
    public int ATK;
    public bool IsATK;
    public int DEF;
    public bool IsDEF;
    public int SPD;
    public bool IsSPD;
    public int Magazine;
    public int Ammo;
    public bool IsPrimary;

    // 現在のスキルスロット名
    public string[] slotName;
    // スロット名取得
    public string GetSlotName(int _num)
    {
        return slotName[_num];
    }
    // スロット名設定
    public void SetSlotName(string _name, int _num)
    {
        slotName[_num] = _name;
    }
    // パラメーター取得
    public object GetParameter(string _name)
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
    }// 引数_nameと同じ変数名の値をstringで返す
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
    public void SetParameter(string _name, object _value)
    {
        switch (_value)
        {
            case int n when _name == "HP":
                HP += (int)_value;
                break;
            case int n when _name == "ATK":
                ATK += (int)_value;
                break;
            case int n when _name == "DEF":
                DEF += (int)_value;
                break;
            case int n when _name == "SPD":
                SPD += (int)_value;
                break;
            case int n when _name == "Magazine":
                Magazine += (int)_value;
                break;
            case int n when _name == "Ammo":
                Ammo += (int)_value;
                break;
            case bool b when _name == "IsATK":
                IsATK = (bool)_value;
                break;
            case bool b when _name == "IsDEF":
                IsDEF = (bool)_value;
                break;
            case bool b when _name == "IsSPD":
                IsSPD = (bool)_value;
                break;
            case bool b when _name == "IsPrimary":
                IsPrimary = (bool)_value
;
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
