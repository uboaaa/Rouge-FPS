using UnityEngine;

public class Parameter : MonoBehaviour
{
    // 各パラメーター・・・それぞれの変数名＝テクスチャの名前
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
    // 入っているパラメーターの名前(Skill,Slot用)
    public string Name;
    public string GetName()
    {
        return Name;
    }
    public void SetName(string _name)
    {
        Name = _name;
    }

    // パラメーター設定
    // 引数_nameと同じ変数名に_valueを入れる
    public void SetParameter(string _name, string _value)
    {
        var param = Conversion(_name, _value);
        Set(_name, param);
    }
    // _nameと同じ変数名に_statusを設定する
    public void Set(string _name,object _status)
    {
        switch(_status)
        {
            case int n when _name == "HP":
                HP += (int)_status;
                break;
            case int n when _name == "ATK":
                ATK += (int)_status;
                break;
            case int n when _name == "DEF":
                DEF += (int)_status;
                break;
            case int n when _name == "SPD":
                SPD += (int)_status;
                break;
            case int n when _name == "Magazine":
                Magazine += (int)_status;
                break;
            case int n when _name == "Ammo":
                Ammo += (int)_status;
                break;
            case bool b when _name == "IsATK":
                IsATK = (bool)_status;
                break;
            case bool b when _name == "IsDEF":
                IsDEF = (bool)_status;
                break;
            case bool b when _name == "IsSPD":
                IsSPD = (bool)_status;
                break;
            case bool b when _name == "IsPrimary":
                IsPrimary = (bool)_status;
                break;
        }
    }
    // stringから各パラメーターに対応した型に変換する
    public object Conversion(string _name, string _value)
    {
        switch(_name)
        {
            case  "HP":
                return int.Parse(_value);
            case "ATK":
                return int.Parse(_value);
            case "DEF":
                return int.Parse(_value);
            case "SPD":
                return int.Parse(_value);
            case "Magazine":
                return int.Parse(_value);
            case "Ammo":
                return int.Parse(_value);
            case "IsATK":
                return _value == "true" ? true : false;
            case "IsDEF":
                return _value == "true" ? true : false;
            case "IsSPD":
                return _value == "true" ? true : false;
            case "IsPrimary":
                return _value == "true" ? true : false;
        }
        return "err";
    }
    // 引数_nameと同じ変数名の値をstringで返す
    public string GetParameter(string _name)
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
    // objectで返す
    public object GetParameterToObject(string _name)
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

    // すべてのパラメーターをクリア
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
    }
}
