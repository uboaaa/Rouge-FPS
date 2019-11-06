using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PushKey : MonoBehaviour
{
    // 仮レベルアップフラグ
    private bool levelFlag;
    // スキルスロット
    private string[] ssText;
    private GameObject[] skillSlot;
    // 抽選用
    private Lottery lot = null;
    private List<string[]> skillList = null;
    // CSV読み書き
    private CSV csv = null;

    private GameObject[] text = null;

    // Start is called before the first frame update
    void Start()
    {
        lot = new Lottery();
        levelFlag = false;
        // 確定しているスキルのテキスト表示用
        ssText = new string[3];
        ssText[0] = "none";
        ssText[1] = "none";
        ssText[2] = "none";
        skillSlot = new GameObject[3];
        skillSlot[0] = GameObject.Find("SkillSlot1");
        skillSlot[1] = GameObject.Find("SkillSlot2");
        skillSlot[2] = GameObject.Find("SkillSlot3");

        skillList = new List<string[]>();
        csv = new CSV();
        /*
        text = new GameObject[6];
        text[0] = GameObject.Find("Skill1-2");
        text[1] = GameObject.Find("Skill2-2");
        text[2] = GameObject.Find("Skill3-2");
        text[3] = GameObject.Find("Skill2-2");
        text[4] = GameObject.Find("Skill3-1");
        text[5] = GameObject.Find("Skill3-2");
        */
    }

    public List<string[]> GetList()
    {
        return skillList;
    }
    public void SetList(ref List<string[]> _list)
    {
        skillList = _list;
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力でレベルアップ判定
        if(Input.GetKeyDown(KeyCode.L))
        {
            levelFlag = true;
        }
        // レベルアップ時
        if(levelFlag)
        {
            // いったん確認
            for (int i = 0; i < 3; i++) 
            {
                // グレード抽選
                var grade = lot.GradeLottery("0", 4, 3, 3);
                // スキル抽選
                var rnd = lot.SkillLottery(grade);
                // 仮文字表示
                ssText[i] = rnd;
            }
            levelFlag = false;
        }
        // Pキーでリセット
        if (Input.GetKey(KeyCode.P))
        {
            for(int i = 0; i < 3; i++)
            {
                ssText[i] = "none";
            }
        }
        // くっついているスキル表示
        for (var i = 0; i < 3; i++)
        {
            skillSlot[i].GetComponent<Text>().text = ssText[i];
        }
    }
}
