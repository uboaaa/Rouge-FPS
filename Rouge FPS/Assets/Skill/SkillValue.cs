using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// スキルの効果値描画
public class SkillValue : MonoBehaviour
{
    public GameObject parent;
    private string value = "";
    public Text text;

    private void Awake()
    {
        // コンポーネント取得
        text = GetComponent<Text>();
    }
    
    // UI設置時に呼び出す
    public void SetText()
    {
        // パラメータの数値を取得
        var param = parent.GetComponent<Parameter>();
        var name = param.GetName();
        value = parent.GetComponent<Parameter>().GetParameter(name);
        // 例外は文字無し
        if (value == "err" || value == "none")
        {
            value = "";
        }
        text.text = value;
    }
}
