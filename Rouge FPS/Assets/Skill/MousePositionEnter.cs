using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePositionEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    // クリックしている間の判定
    bool isClick = false;
    // 待機状態
    bool isStandby = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Inside");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Outside");
    }


    public bool CheckStandby()
    {
        return isStandby;
    }

    void Start()
    {

    }

    void Update()
    { 

    }

    // マウスダウン時
    void OnMouseDown()
    {
        isClick = true;
    }
    // マウスアップ時
    void OnMouseUp()
    {
        isClick = false;
    }
    // 衝突時
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "mousePoint") return;
        isStandby = true;
    }
    // 抜けた時
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "mousePoint") return;
        isStandby = false;
    }
}