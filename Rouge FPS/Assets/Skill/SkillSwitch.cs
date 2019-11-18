using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキル抽選後スキルスロットとの入れ替えインターフェース関係
public class SkillSwitch : MonoBehaviour
{
    // スキル入れ替えが発生した場合
    private bool switchFlag = false;
    public void SetFlag(bool _result)
    {
        switchFlag = _result;
    }

    DragDrop dd = new DragDrop();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 
    public void Switch()
    {
        var skill = dd.GetSkillName();
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
