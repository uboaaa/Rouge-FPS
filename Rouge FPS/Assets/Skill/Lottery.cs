using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 抽選
// すべてこっちの関数で変更処理を行う
// スキルのグレード（小さい数字ほどグレードの高いスキル）を抽選して
// 選ばれたグレードの中からランダムで１つスキルを抽選する
// グレードは重みとしても計算する
public class Lottery : MonoBehaviour
{
    // CSV読み込みと保存リスト
    private List<string[]> list = new List<string[]>();
    private CSV csv = new CSV();
    // enumで用意
    public enum SKILL
    {
        // 能力(ATK,DEF...等)
        Ability = 0,
        // 効果値(int)
        Parameter = 1,
        // 表示するテクスチャの名前
        Name = 2
    };

    // =============
    // 抽体の関数
    // =============
    // スキルのグレードを決める・・・（おおざっぱなカテゴリに分ける）数字が大きいほど出やすい
    // ※　ratioTableは抽選のアルゴリズムの関係上、必ず降順(5,4,3･･･)で渡すこと！　※
    // 第1引数は最低グレードです(1を指定すると1以上のグレードを返す)
    // ex) var i = GradeLottery("3", 94, 5, 1)の場合94%で3、5%で4、1%で5を返します。
    public int GradeLottery(string _grade, params int[] ratioTable)
    {
        // グレードが数字かチェック
        var result = int.TryParse(_grade, out int n);
        int grade = result ? int.Parse(_grade) : n;
        // 渡された重みでインデックス作成
        var totalRatio = ratioTable.Sum();
        // 総重みの範囲内でランダムで値を返す（int型のRandom.Rangeはmax-1の値まで返すため+1）
        var value = Random.Range(1, totalRatio + 1);
        // 結果
        var retIndex = -1;
        // valueより大きい場合結果を確定する
        for (var i = 0; i < ratioTable.Length; ++i) 
        {
            if(ratioTable[i] >= value)
            {
                retIndex = i;
                break;
            }
            value -= ratioTable[i];
        }
        return retIndex + grade;
    }
    // 付けるスキルそのものを抽選する
    public string[] SkillLottery(int _grade)
    {
        // グレード文字結合
        var s = "Grade" + _grade.ToString();
        // CSV読み込み
        csv.Read(ref list, s);
        // 行数取得、ランダム抽選
        var len = list.Count();
        var rnd = Random.Range(1, len + 1);
        return list[rnd];
    }
    // 上の関数二つをまとめたもの
    public string[] LevelUp(string _grade, params int[] _ratioTable)
    {
        var grade = GradeLottery(_grade, _ratioTable);
        var skill = SkillLottery(grade);
        return skill;
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
