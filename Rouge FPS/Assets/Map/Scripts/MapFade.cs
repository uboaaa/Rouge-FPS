using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapFade : MonoBehaviour
{
    //フェード用のCanvasとImage
    private static Canvas m_FadeCanvas;

    private static Image m_FadeImage;

    //フェード用パネルの透明度
    private static float fadeAlpha = 0.0f;

    //フェードインのフラグ
    private static bool isFadeIn = false;
    //フェードアウトのフラグ
    private static bool isFadeOut = false;

    //フェードしたい時間
    private static float fadeTime = 2.0f;

    //フェード用のCanvasとImage生成
    static void Init()
    {
        //フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        m_FadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        m_FadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<MapFade>();

        //最前面になるよう適当なソートオーダー設定
        m_FadeCanvas.sortingOrder = 100;

        //フェード用のImage生成
        m_FadeImage = new GameObject("ImageFade").AddComponent<Image>();
        m_FadeImage.transform.SetParent(m_FadeCanvas.transform, false);
        m_FadeImage.rectTransform.anchoredPosition = Vector3.zero;

        //Imageサイズは適当に大きく設定してください
        m_FadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }

    // Update is called once per frame
    void Update()
    {
        //フラグ有効なら毎フレームフェードイン/アウト処理
        if (isFadeIn)
        {
            //経過時間から透明度計算
            fadeAlpha -= Time.deltaTime / fadeTime;

            //フェードイン終了判定
            if (fadeAlpha <= 0.0f)
            {
                isFadeIn = false;
                fadeAlpha = 0.0f;
                m_FadeCanvas.enabled = false;
            }

            //フェード用Imageの色・透明度設定
            m_FadeImage.color = new Color(1.0f, 1.0f, 1.0f, fadeAlpha);
        }
        else if (isFadeOut)
        {
            //経過時間から透明度計算
            fadeAlpha += Time.deltaTime / fadeTime;

            //フェードアウト終了判定
            if (fadeAlpha >= 1.0f)
            {
                isFadeOut = false;
                fadeAlpha = 1.0f;
            }

            //フェード用Imageの色・透明度設定
            m_FadeImage.color = new Color(1.0f, 1.0f, 1.0f, fadeAlpha);
        }
    }

    //フェードイン開始
    public static void FadeIn()
    {
        if (m_FadeImage == null) Init();
        isFadeIn = true;
        isFadeOut = false;
        m_FadeImage.color = Color.white;
    }

    //フェードアウト開始
    public static void FadeOut()
    {
        isFadeOut = true;
        isFadeIn = false;
        m_FadeImage.color = Color.clear;
        m_FadeCanvas.enabled = true;
    }

    //フェード進捗確認
    public static float FadeProgress()
    {
        return fadeAlpha;
    }
}
