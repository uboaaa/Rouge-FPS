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
            // 名前変更
            param.SetSlotName(name, i);
            // 値変更
            Debug.Log(name + ", " + slotParam.GetParameterToObject(name));
            param.SetParameter(name, slotParam.GetParameterToObject(name));
        }
        GameObject.Find("UI").SetActive(false);
    }

}
