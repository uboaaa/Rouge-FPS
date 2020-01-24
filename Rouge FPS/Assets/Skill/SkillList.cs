using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルリスト関係
public class SkillList : MonoBehaviour
{

    public class Skill
    {
        // 能力名
        public string skillName;
        // 効果値
        public float parameter;
        // 重み（数値が大きいほど出やすい）
        public int ratio;
        // 確率
        public float probability;
        // 最低出現階層
        public int lowAppear;
        // 最高出現階層
        public int highAppear;
        

        // 初期化
        public void Init(string _name, float _param, int _ratio, int _low, int _high)
        {
            skillName = _name;
            parameter = _param;
            ratio = _ratio;
            lowAppear = _low;
            highAppear = _high;
        }
    };

    // 現在の階層で出現するスキルのリスト
    private List<Skill> nowFloorSkill = null;
    // 総重み
    private float totalRatio;
    // スキルのグレード
    private int[] skillGrade;
    public int[] GetSkillGrade()
    {
        return skillGrade;
    }

    // スキルリスト
    private int[][] skillList;
    // スキルスロット
    private int[] skillSlot;
    // スキル設定
    public void SetSkillSlot(int _slot, int _grade, int _result)
    {
        skillSlot[_slot] = skillList[_grade][_result];
    }


    // リスト取得
    public List<Skill> GetList()
    {
        return nowFloorSkill;
    }
    //// 現在の階層のスキルセット
    //public void SetList(int _nowFloor)
    //{
    //    // 重みリセット
    //    totalRatio = 0.0f;
    //    // リストの値すべてを検索
    //    foreach (Skill value in skillList.Values)
    //    {
    //        // 出現階層かどうか判定
    //        if (value.lowAppear > _nowFloor) continue;
    //        if (value.highAppear < _nowFloor) continue;
    //        // 出現するスキルの重みを足し込む
    //        totalRatio += value.ratio;
    //        // リストに追加
    //        nowFloorSkill.Add(value);
    //    }
    //}
    // スキルリストのリセット
    public void ListReset()
    {
        nowFloorSkill.Clear();
    }
    // いらないかも（？）
    // スキルの出現確率
    public void ProbabilitySet()
    {
        // 出現階層のスキルから
        foreach (Skill s in nowFloorSkill)
        {
            // 出現確率 ＝ スキルの重み / すべての重み
            s.probability = s.ratio / totalRatio;
        }
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
