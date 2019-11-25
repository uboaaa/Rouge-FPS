using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ミニマップ用マップチップクラス
public class MapTip : MonoBehaviour
{
    private int tip_x, tip_y;                                               //チップの配置データ(配列の要素の検索用)

    private Selectable<bool> m_tipEnable = new Selectable<bool>();          //チップOn/Off

    private Image m_tipImage = null;                                        //チップのImageコンポーネント
    private RectTransform m_tipRect = null;                                 //チップのRectコンポーネント

    // 初期化
    void Start()
    {
        //イメージコンポーネントを取得
        m_tipImage = this.gameObject.GetComponent<Image>();
        //Rectコンポーネントを取得
        m_tipRect = this.gameObject.GetComponent<RectTransform>();

        //チップOn/Off切替時に呼び出す関数を設定
        m_tipEnable.mChanged += value => m_tipImage.enabled = value;
        //チップのImageコンポーネントをOffにする
        m_tipEnable.Value = false;
    }

    // チップの座標設定
    // 引数：配列の要素をセット
    void SetTip(int x,int y)
    {
        tip_x = x;
        tip_y = y;
        float posX = x * m_tipRect.sizeDelta.x;
        float posY = y * m_tipRect.sizeDelta.y;
        m_tipRect.localPosition = new Vector3(posX, posY);
    }
}
