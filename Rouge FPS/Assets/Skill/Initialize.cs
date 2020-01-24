using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// =================================
// スキルの変更画面
// =================================
public class Initialize : MonoBehaviour
{
    // スキルの配置
    // 武器側
    [SerializeField] float weaponX1;
    [SerializeField] float weaponY1;
    [SerializeField] float weaponX2;
    [SerializeField] float weaponY2;
    [SerializeField] float weaponX3;
    [SerializeField] float weaponY3;
    private Vector3[] WeaponPos = new Vector3[Parameter.SLOTMAX];

    // 抽選スキル側
    [SerializeField] float lotteryX1;
    [SerializeField] float lotteryY1;
    [SerializeField] float lotteryX2;
    [SerializeField] float lotteryY2;
    [SerializeField] float lotteryX3;
    [SerializeField] float lotteryY3;
    private Vector3[] LotPos = new Vector3[Parameter.SLOTMAX];

    // レベルアップ時表示
    private bool levelUpFlag = false;
    public void IsLevelUp(bool _result)
    {
        levelUpFlag = _result;
    }
    Parameter param = new Parameter();

    // プレハブの名前
    // WEAPONSLOTは枠だけ
    [SerializeField] string WEAPONSLOT;
    GameObject skillSlot;
    string[] spriteName;

    // Start is called before the first frame update
    void Start()
    {
        // 初期座標
        WeaponPos[0].x = weaponX1;
        WeaponPos[0].y = weaponY1;
        WeaponPos[1].x = weaponX2;
        WeaponPos[1].y = weaponY2;
        WeaponPos[2].x = weaponX3;
        WeaponPos[2].y = weaponY3;

        LotPos[0].x = lotteryX1;
        LotPos[0].y = lotteryY1;
        LotPos[1].x = lotteryX2;
        LotPos[1].y = lotteryY2;
        LotPos[2].x = lotteryX3;
        LotPos[2].y = lotteryY3;


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
