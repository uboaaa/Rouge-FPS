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
    // スキルリスト
    private SkillList sl;
    // 重みテーブル
    private List<int> ratioTable;       // いらないかも？
    // グレードをいれる配列(GradeLotteryで渡す変数)
    private int[] skillGrade;
    // グレード毎のスキルリスト
    private List<int> gradeList;
    // 付与されるスキル
    private int skillResult = 0;

    // 抽選準備・・・現在いる階層の出現するスキルリストから重みテーブルを作成
    public void Init()
    {
        var i = 0;
        // 出現スキルすべての重みを計算
        foreach (SkillList.Skill s in sl.GetList())
        {
            ratioTable[i++] += s.ratio;
        }
    }

    // =============
    // 抽体の関数
    // =============
    // スキルのグレードを決める・・・（おおざっぱなカテゴリに分ける）
    // ※　ratioTableは抽選のアルゴリズムの関係上、必ず降順(5,4,3･･･)で渡すこと！　※
    public int GradeLottery(params int[] ratioTable)
    {
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
        return retIndex;
    }
    // 付けるスキルそのものを抽選する
    public int SkillLottery(int _grade)
    {
        skillResult = 0;
        // グレードごとのスキルをまとめたリストを取得
        // var list = GetList(_grade)的なやつ
        // var len = list.Length;
        // skillResult = Random.Range(1, len + 1);
        return skillResult;
    }
    // 一連の流れをまとめる
    public void TotalLottery()
    {
        var s = 0;
        skillGrade = sl.GetSkillGrade();
        s = SkillLottery(GradeLottery(skillGrade));


    }

    // Start is called before the first frame update
    void Start()
    {
        sl = new SkillList();
    }

    // Update is called once per frame
    void Update()
    {
        // お試し
        if (Input.GetKey(KeyCode.Return))
        {

            var count = new int[3];
            for (int i = 0; i < 100000; ++i)
            {
                var index = GradeLottery(1000, 50, 1);
                count[index]++;
            }

            for (int index = 0; index < count.Length; index++)
            {
                Debug.Log(index + ":" + count[index]);
            }
        }
    }
}
