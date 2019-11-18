using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssistanceMessageManager : MonoBehaviour
{
    /*
    //前フレームでヒットしたヘルプが必要なオブジェクト
    private GameObject _preFrameAssistanceObject = null;
    //uiにRayがあたったか結果格納リスト
    private readonly List<RaycastResult> rayResult = new List<RaycastResult>();
    [SerializeField] private Text _helpMessage;

    void Update()
    {
        rayResult.Clear();

        var currentPointData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        EventSystem.current.RaycastAll(currentPointData, rayResult);

        //Rayがあたったオブジェクトから目的のインターフェースを持ったものがあればtextを更新
        if (_preFrameAssistanceObject == null)
        {
            foreach (var raycastResult in rayResult)
            {
                var assistance = raycastResult.gameObject.GetComponent<INeedAssistance>();
                if (assistance != null)
                {
                    _preFrameAssistanceObject = raycastResult.gameObject;
                    _helpMessage.text = assistance.AssistMessage;
                    _helpMessage.transform.position = currentPointData.position;
                    _helpMessage.transform.gameObject.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            //前フレームにあたったオブジェクトがヒットしていなければuiから離れたとみなして非表示にする
            if (rayResult.All(ray => ray.gameObject != _preFrameAssistanceObject))
            {
                _helpMessage.transform.gameObject.SetActive(false);
                _preFrameAssistanceObject = null;
            }
        }
    }

    private void OnDestroy()
    {
        rayResult.Clear();
        _preFrameAssistanceObject = null;
    }
    */
}