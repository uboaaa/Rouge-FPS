using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTextManager : MonoBehaviour
{
    Action action;
    GameObject childObject;
    void Start()
    {
        GameObject anotherObject = GameObject.Find("FPSController");
        action = anotherObject.GetComponent<Action>();

        childObject = transform.Find("GetText").gameObject;
    }

    void Update()
    {
        if(action.actionFlg)
        {
            childObject.SetActive(true);
        }
    }
}
