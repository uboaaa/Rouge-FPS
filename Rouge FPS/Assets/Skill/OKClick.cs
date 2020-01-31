using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKClick : MonoBehaviour
{
    public GameObject[] SlotObj;
    [Header("-----------------------------------------")]
    public GameObject Parameter;

    public void SkillChange()
    {
        // プレイヤーのパラメーター取得
        var param = Parameter.GetComponent<PlayerParameter>();
        param.AllReset();
        for (var i = 0; i < 3; i++)
        {
            // i番目のスキルのパラメーター取得
            var slotParam = SlotObj[i].GetComponent<Parameter>();
            // 名前取得
            var name = slotParam.GetName();
            // i番目スキルの名前に変更(次回描画時に切り替わる)
            param.SetSlotName(name, i);
            // i番目スキル値の変更
            Debug.Log(name + ", " + slotParam.GetParameterToObject(name));
            param.SetParameter(name, slotParam.GetParameterToObject(name), i);
        }
        GameObject.Find("UI").SetActive(false);
    }

}
