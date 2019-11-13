using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// スキルドラッグ時
public class DragDrop : MonoBehaviour 
{
    private Vector3 screenPoint;
    private Vector3 offset;
    // 座標記憶用
    private Vector3 defaultPosition;
    // 記憶用
    private SpriteRenderer currentRenderer;
    private Sprite currentSprite;
    // マウスボタンを離したときスキルがセットされる位置かどうか
    private bool setFlag = false;
    // アルファ値
    [SerializeField] private readonly float alpha = 0.5f;
    // ドラッグ中判定
    private bool dragFlag = false;

    // マウスクリックしたとき
    void OnMouseDown()
    {
        // 透明度を上げて表示する
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        // 直前の座標を取得
        defaultPosition = transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    // マウスボタンを離したとき
    void OnMouseUp()
    {
        // ドラッグ中フラグオフ
        dragFlag = false;
        // 透明度を下げて表示しない
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        // 元の位置に戻る
        transform.position = this.defaultPosition;
    }

    // ドラッグしている間
    void OnMouseDrag()
    {
        // ドラッグ中フラグオン
        dragFlag = true;
        // ドラッグ中は常に座標取得・更新
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentPosition;
    }

    // ドロップ可能エリアに入った時
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "DroppableField") return;
        // 直前の画像を保存
        currentRenderer = col.GetComponent<SpriteRenderer>();
        currentSprite = currentRenderer.sprite;
    }

    // ドロップ可能エリア内にいる間
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "DroppableField") return;
        // マウスの座標が対象の矩形内かどうか
        //var check = col.GetComponent<MousePositionEnter>().CheckStandby();
        //if (!check) return;
        // マウスボタン推してるとき
        

        // 取得
        var my = this.GetComponent<SpriteRenderer>();
        // ドラッグ中の画像に変える
        currentRenderer.sprite = my.sprite;
        // 色の変更
        currentRenderer.color = new Color(1, 1, 1, 0.5f);

    }

    // ドロップ可能エリア外に出た時
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "DroppableField") return;
        // 直前の画像・色に戻す
        currentRenderer.sprite = currentSprite;
        currentRenderer.color = new Color(1, 1, 1, 1);
    }

   



    void Update()
    {
        /*
        Vector3 touchScreenPosition = Input.mousePosition;

        // 10.0fに深い意味は無い。画面に表示したいので適当な値を入れてカメラから離そうとしているだけ.
        touchScreenPosition.z = 10.0f;

        Camera gameCamera = Camera.main;
        Vector3 touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);

        Debug.Log(touchWorldPosition.x + " : " + touchWorldPosition.y);

        var obj = GameObject.Find("Choice2");
        obj.transform.position = touchWorldPosition;
//        Debug.Log("X : " + obj.transform.position.x + ", Y : " + obj.transform.position.y);
*/
    }
}
