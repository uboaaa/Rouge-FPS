using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルリスト関係
public class SkillList : MonoBehaviour
{

    class Skill
    {
        // 能力名
        public string skillName;
        // 効果値
        public float parameter;
        // 重み（数値が大きいほど出やすい）
        public float weight;
        // 確率
        public float probability;
        // 最低出現階層
        public int lowAppear;
        // 最高出現階層
        public int highAppear;

        // コンストラクタ
        public Skill() { }

        // 初期化
        public void Init(string _name, float _param, float _weight, int _low, int _high)
        {
            skillName = _name;
            parameter = _param;
            weight = _weight;
            lowAppear = _low;
            highAppear = _high;
        }
    };

    // すべてのスキルのリスト
    private Dictionary<string, Skill> skillList;
    // 現在の階層で出現するスキルのリスト
    private List<Skill> nowFloorSkill = null;
    // 総重み
    private float allWeight;

    // リスト取得
    List<Skill> GetList()
    {
        return nowFloorSkill;
    }
    // 現在の階層のスキルセット
    void SetList(int _nowFloor)
    {
        // 重みリセット
        allWeight = 0.0f;
        // リストの値すべてを検索
        foreach (Skill value in skillList.Values)
        {
            // 出現階層かどうか判定
            if (value.lowAppear > _nowFloor) continue;
            if (value.highAppear < _nowFloor) continue;
            // 出現するスキルの重みを足し込む
            allWeight += value.weight;
            // リストに追加
            nowFloorSkill.Add(value);
        }
    }
    // スキルリストのリセット
    void ListReset()
    {
        nowFloorSkill.Clear();
    }
    // スキルの出現確率
    void ProbabilitySet()
    {
        // 出現階層のスキルから
        foreach (Skill s in nowFloorSkill)
        {
            // 出現確率 ＝ スキルの重み / すべての重み
            s.probability = s.weight / allWeight;
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
