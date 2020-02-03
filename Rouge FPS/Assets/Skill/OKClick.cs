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
            param.SetParameter(name, slotParam.GetParameterToObject(name), i);
            // スロット側初期化設定
            slotParam.GetComponent<DropUI>().InitParameter();
        }
        // UI再度ドロップ可能状態にする
        DropUI.UnLock();
        UIManager.SetFalseUIFlg();
        MyStatus.NewMaxHP();
        // GameObject.Find("UI").SetActive(false);
        //次のマップへ生成・移動
        MapInitializer.MoveNextMap();
    }

    public void EndAnimation()
    {
        // マウスカーソルの設定を元に戻す
        Cursor.lockState = CursorLockMode.Locked;
    }


}
