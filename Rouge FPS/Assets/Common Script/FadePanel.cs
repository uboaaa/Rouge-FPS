using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadePanel : MonoBehaviour
{
    float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
    float red, green, blue;
    public static float alfa;   //パネルの色、不透明度を管理

    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
    public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

    private bool Black;
    Image fadeImage;                //透明度を変更するパネルのイメージ

    GameObject gameObject;
    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
        gameObject = GameObject.Find("GameObject");
    }

    void Update()
    {
        

        
                if (GametoTitle.GetModeSelect()=="ReturnTitle" || TitletoGame.GetGame() || SceneManager.GetActiveScene().name=="GameOverScene")
                {
                    if (Input.GetKeyDown(KeyCode.Return) ) {
                    isFadeOut = true;
                    }
                }
                else { isFadeOut = false; }
            
        
        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
        SetAlpha();                      //b)変更した不透明度パネルに反映する
        if (alfa <= 0)
        {                    //c)完全に透明になったら処理を抜ける
            isFadeIn = false;
            fadeImage.enabled = false;    //d)パネルの表示をオフにする
        }
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        alfa += fadeSpeed;         // b)不透明度を徐々にあげる
        SetAlpha();               // c)変更した透明度をパネルに反映する
        if (alfa >= 1)
        {             // d)完全に不透明になったら処理を抜ける
            isFadeOut = false;
            Black = true;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
    public  bool GetAllBlack() { return Black; }

    public static float AlphaGet(){return alfa;}
}
