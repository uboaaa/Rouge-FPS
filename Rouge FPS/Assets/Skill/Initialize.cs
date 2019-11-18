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
    GameObject skillSlot = new GameObject();
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
        skillSlot = (GameObject)Resources.Load(WEAPONSLOT);
        string s;
        // 現在のついているスキル設定
        for (var i = 0; i < Parameter.SLOTMAX; i++)
        {
            s = "Skill/" + param.GetSkillSlot()[i].name;
            skillSlot.GetComponent<SpriteRenderer>().sprite = null;
            skillSlot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(s);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.A))
        {
            levelUpFlag = true;
        }
        // レベルアップ時
        if(levelUpFlag)
        {
            levelUpFlag = false;
            // スプライト配置　細かい演出はこの辺に書く
            for (var i = 0; i < Parameter.SLOTMAX; i++)
            {
                // 現在のスキルスプライト
                Instantiate(skillSlot, new Vector3(WeaponPos[i].x, WeaponPos[i].y, 0), Quaternion.identity);
            }
        }
    }
}
