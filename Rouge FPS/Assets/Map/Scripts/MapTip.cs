using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ミニマップ用マップチップクラス
public class MapTip : MonoBehaviour
{
    private int tip_x = 0, tip_y = 0;                                       //チップの配置データ(配列の要素の検索用)
    private int correct = 30;                                               //***マップチップの配置データ補正(※あとで自動設定にする)

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
        m_tipEnable.Value = true;

        //配置データを座標に変換し、設定する
        float posX = (tip_x - correct) * m_tipRect.sizeDelta.x;
        float posY = (tip_y - correct) * m_tipRect.sizeDelta.y;
        //アンカーを基準に座標を設定
        m_tipRect. anchoredPosition = new Vector3(posX, posY);
    }

    // チップの座標設定
    // 引数：配列の要素をセット
    public void Initialize(int x,int y)
    {
        tip_x = x;
        tip_y = y;
    }
}
