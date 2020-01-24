using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

// UI管理クラス
public class UIManager : MonoBehaviour
{
    public GameObject[] SkillPrefab;
    [SerializeField] Vector2[] SkillPosition;
    [Header("-----------------------------------------")]
    public GameObject[] SlotPrefab;
    [SerializeField] Vector2[] SlotPosition;
    [Header("-----------------------------------------")]
    public GameObject[] ButtonPrefab;
    [SerializeField] Vector2[] ButtonPosition;

    private Lottery lot;
    private string[] randomSkill;

    List<GameObject> prefabList;

    void Start()
    {
        lot = new Lottery();
        randomSkill = new string[3];
        prefabList = new List<GameObject>();
    }

    public void Test()
    {
        // プレハブ生成
        // スキルUI
        GameObject tmp;
        var i = 0;
        foreach (var prefab in SkillPrefab)
        {
            tmp = Instantiate(prefab, SkillPosition[i], Quaternion.identity, GameObject.Find("SkillCanvas").transform);
            tmp.name = prefab.name;
            // テクスチャ読み込み
            var s = "Skill/" + randomSkill[i];
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>(s);
            prefabList.Add(tmp);
            i++;
        }
        // スロットUI
        i = 0;
        foreach (var prefab in SlotPrefab)
        {
            tmp = Instantiate(prefab, SlotPosition[i], Quaternion.identity, GameObject.Find("SlotCanvas").transform);
            tmp.name = prefab.name;
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("Skill/edge");
            prefabList.Add(tmp);
            i++;
        }
        // ボタンUI
        i = 0;
        foreach (var prefab in ButtonPrefab)
        {
            tmp = Instantiate(prefab, ButtonPosition[i], Quaternion.identity, GameObject.Find("ButtonCanvas").transform);
            tmp.name = prefab.name;
            prefabList.Add(tmp);
            i++;
        }
    }

    private void OnClick()
    {
        Debug.Log("Enter");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (var i = 0; i < 3; i++)
            {
                randomSkill[i] = lot.LevelUp("0", 4, 3, 3)[0];
            }
            Test();
        }

        // フラグの初期化とかしてないけどいったん削除
        if(Input.GetKeyDown(KeyCode.S))
        {
            foreach (var prefab in prefabList) 
            {
                Destroy(prefab);
            }
        }

    }

}
