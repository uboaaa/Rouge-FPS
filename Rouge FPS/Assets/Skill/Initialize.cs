using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    // スキルの配置
    private Vector3[] SkillPos = new Vector3[Parameter.SLOTMAX];
    private Vector3[] WeaponPos = new Vector3[Parameter.SLOTMAX];

    private bool levelUpFlag = false;
    public void isLevelUp(bool _result)
    {
        levelUpFlag = _result;
    }
    Parameter param = new Parameter();

    // プレハブの名前
    const string WEAPONSLOT = "Skill/WeaponSlot";

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
        GameObject skillSlot = (GameObject)Resources.Load(WEAPONSLOT);
        // スプライト変更
        var sprite = skillSlot.GetComponent<SpriteRenderer>().sprite;
        // var skill = param.GetSkillSlot();
        //string s;
        // 現在のついているスキル設定
        //for (var i = 0; i < Parameter.SLOTMAX; i++)
        //{
            //s = "Skill/" + skill[i].name;
            sprite = null;
            sprite = Resources.Load<Sprite>("Skill/SPD");
        // 生成
            Instantiate(skillSlot, new Vector3(WeaponPos[0].x, WeaponPos[0].y, 0), Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(levelUpFlag)
        {
            // スプライト配置

        }
    }
}
