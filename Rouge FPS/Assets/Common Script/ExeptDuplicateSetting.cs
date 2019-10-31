using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームシーンで重複するオブジェクトに付ける
//マージ時に自動で非表示する
public class ExeptDuplicateSetting : MonoBehaviour
{
    private GameObject duplicateObj;
    private bool active = false;

    void Start()
    {
        //MergeScenesスクリプトにフラグ作り直す
        active = MergeScenes.IsMerge();

        duplicateObj = this.gameObject;
        if (active == true)
        {
            duplicateObj.SetActive(false);
        }
    }
}
