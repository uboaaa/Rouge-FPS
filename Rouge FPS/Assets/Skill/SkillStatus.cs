using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillStatus : ScriptableObject
{
    // スキルパラメータ
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
    
    // CSVのデータ順
    public enum CSVParameter
    {
        NAME = 0,
        VALUE = 1,
        ACTIVE = 2,
        SLOTNUMBER = 3
    };
    // スキルスロット
    private const int SLOTMAX = 3;
    public class SkillSlot
    {
        // スプライトの名前
        public string name = "";
        // 効果
        public int value = 0;
        // スキルスロット枠が使えるかどうか(1:ON 0:OFF)
        public int active = 0;
    }
    public SkillSlot[] skillSlot { get; set; } = new SkillSlot[SLOTMAX];
    
}
