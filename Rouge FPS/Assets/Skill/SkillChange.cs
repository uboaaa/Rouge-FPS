using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ======================================
// スキル変更画面
// 
// 左(現在の武器スキル)に右(新しいスキル)をドラッグアンドドロップした際の処理等
// 
// ======================================

public class SkillChange : MonoBehaviour
{
    // スキルのスプライトを配置する座標
    // 抽選されたスキル
    private Vector3[] SkillPos = new Vector3[Parameter.SLOTMAX];
    // 現在のスキル
    private Vector3[] WeaponPos = new Vector3[Parameter.SLOTMAX];

    // レベルアップ判定
    private bool levelUpFlag = false;
    public void isLevelUp(bool _result)
    {
        levelUpFlag = _result;
    }

    // パラメータから現在のスキルを取得するため
    Parameter param = new Parameter();

    // プレハブの名前
    const string WEAPONSLOT = "Skill/WeaponSlot";
    GameObject skillSlot;
    // スプライト(スキルの名前)保存用
    string[] spriteName;

    // Start is called before the first frame update
    void Start()
    {
        // 初期座標
        WeaponPos[0].x = -5;
        WeaponPos[0].y = 4;
        WeaponPos[1].x = -5;
        WeaponPos[1].y = 1;
        WeaponPos[2].x = -5;
        WeaponPos[2].y = -2;
        // プレハブ読み込み
        skillSlot = (GameObject)Resources.Load(WEAPONSLOT);
        spriteName = new string[3];
        // parameter
        param = GetComponent<Parameter>();
    }

    // Update is called once per frame
    void Update()
    { 
        // レベルアップ判定
        if(Input.GetKeyDown(KeyCode.A))
        {
            levelUpFlag = true;
            for (var i = 0; i < Parameter.SLOTMAX; i++)
            {
                // その時のスキルスロットの画像名を保存
                spriteName[i] = "Skill/" + param.GetNowSlot()[i].name;
            }
        }
        // 描画演出
        if(levelUpFlag)
        {
            levelUpFlag = false;
            // スプライト配置
            for (var i = 0; i < Parameter.SLOTMAX; i++)
            {
                // 現在のスキルスプライト
                skillSlot.GetComponent<SpriteRenderer>().sprite = null;
                skillSlot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteName[i]);
                Instantiate(skillSlot, new Vector3(WeaponPos[i].x, WeaponPos[i].y, 0), Quaternion.identity);
            }
        }
    }
}
