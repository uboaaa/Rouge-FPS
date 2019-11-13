using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    GameObject WorldObject;
    //this is the ui element
    RectTransform UI_Element;
    GameObject canvas;
    //first you need the RectTransform component of your canvas
    RectTransform CanvasRect;
    Camera cam;
    GameObject text;



    void Start()
    {
        canvas = GameObject.Find("Canvas");
        CanvasRect = canvas.GetComponent<RectTransform>();
        UI_Element = CanvasRect.GetComponent<RectTransform>();
        cam = Camera.main;
        WorldObject = GameObject.Find("Choice1t");
        text = GameObject.Find("Text");
    }

    void Update()
    {
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.



        //Vector2 ViewportPosition = cam.WorldToViewportPoint(WorldObject.transform.position);
        // マウスの座標
        Vector2 pos = WorldObject.transform.position;
        Vector2 ViewportPosition = cam.WorldToViewportPoint(pos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        Vector2 WorldPosition = cam.ViewportToScreenPoint(ViewportPosition);

        //now you can set the position of the ui element
        UI_Element.anchoredPosition = WorldObject_ScreenPosition;

        //string s = "ChoicePosition = " + pos.x + " : " + pos.y + Environment.NewLine;
        string s = "";
        s += "MousePosition = " + Input.mousePosition.x + " : " + Input.mousePosition.y + Environment.NewLine;
        //s += "ViewPortPosition = " + ViewportPosition.x + " : " + ViewportPosition.y + Environment.NewLine;
        //s += "WorldPosition = " + WorldObject_ScreenPosition.x + " : " + WorldObject_ScreenPosition.y + Environment.NewLine;
        s += "WorldPosition = " + WorldPosition.x + " : " + WorldPosition.y + Environment.NewLine;
        
       

        var sprite = WorldObject.GetComponent<SpriteRenderer>();
        float pixel = sprite.sprite.pixelsPerUnit;
        var size = new Vector2(sprite.size.x * pixel, sprite.size.y * pixel);
        var wpos = new Vector2(WorldPosition.x - size.x / 2, WorldPosition.y - size.y / 2);
        var rect = new Rect(wpos, size);

        s += "Size = " + size.x + " : " + size.y + Environment.NewLine;
        s += "1m = " + pixel + Environment.NewLine;
        

        if(rect.Contains(Input.mousePosition))
        {
            s += "INSIDE";
        }
        else
        {
            s += "OUTSIDE";
        }
        

        text.GetComponent<Text>().text = s;
    }
}
