using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    private static List<GameObject> _poolObjList;
    private static GameObject _poolObj;

    // オブジェクトプールを作成
    public static void CreatePool(GameObject obj, int maxCount)
    {
        //プールオブジェクトがnullでなければ、早期リターン
        if (!ReferenceEquals(_poolObj,null)) return;

        _poolObj = obj;
        _poolObjList = new List<GameObject>();
        for (int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewObject();
            newObj.SetActive(false);
            _poolObjList.Add(newObj);
        }
    }

    public static GameObject GetObject()
    {
        // 使用中でないものを探して返す
        foreach (var obj in _poolObjList)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 全て使用中だったら新しく作って返す
        var newObj = CreateNewObject();
        newObj.SetActive(true);
        _poolObjList.Add(newObj);

        return newObj;
    }

    private static GameObject CreateNewObject()
    {
        var newObj = Instantiate(_poolObj);
        newObj.name = _poolObj.name + (_poolObjList.Count + 1);

        return newObj;
    }

}
