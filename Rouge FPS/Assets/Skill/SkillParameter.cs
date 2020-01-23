using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 値だけ
public class SkillParameter : MonoBehaviour
{
    // スキルの名前
    [SerializeField] string skillName;
    public void SetName(string _name)
    {
        skillName = _name;
    }
    public string GetName()
    {
        return skillName;
    }

    // スキルの効果値
    [SerializeField] float skillValue;
    public void SetValue(float _value)
    {
        skillValue = _value;
    }
    public float GetValue()
    {
        return skillValue;
    }
}
