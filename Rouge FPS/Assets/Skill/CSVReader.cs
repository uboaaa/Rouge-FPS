using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CSVReader : MonoBehaviour
{
    // 仮表示用のText(本チャンは削除)
    GameObject skill1 = null;
    GameObject skill2 = null;
    GameObject skill3 = null;
    GameObject skill4 = null;
    GameObject skill5 = null;
    GameObject skill6 = null;
    

    // csvの中身を入れるリスト
    public List<string[]> csvDatas = new List<string[]>();
    public List<string> tempDatas = new List<string>();

    Param.Parameter param;

    void Change()
    {
        tempDatas.Add(param.ATK.ToString());
        csvDatas[1][1] = tempDatas[0];
    }
    void Awake()
    {
        param = Param.GetParameter();
    }

    void Start()
    {
        skill1 = GameObject.Find("Skill1-1");
        skill2 = GameObject.Find("Skill1-2");
        skill3 = GameObject.Find("Skill2-1");
        skill4 = GameObject.Find("Skill2-2");
        skill5 = GameObject.Find("Skill3-1");
        skill6 = GameObject.Find("Skill3-2");
        Read(ref csvDatas, "Read.csv");
    }


    // 一応関数化してます
    // 引数で書き込むリストとファイル名を指定する(拡張子必要)
    public void Read(ref List<string[]> _list, string _pass)
    {
        string s = "Assets/Resources/CSV/" + _pass;
        // csvファイルのパスを指定 
        StreamReader sr = new StreamReader(s);
        // 終わりまで回す
        while(!sr.EndOfStream)
        {
            // 1行読み込み
            string line = sr.ReadLine();
            // カンマ区切り
            string[] values = line.Split(',');
            // 格納
           _list.Add(values);
        }
    }

    public void ChangeParameter()
    {
        // int型変換
        csvDatas[0][1] = param.HP.ToString();
        csvDatas[1][1] = param.ATK.ToString();
        csvDatas[2][1] = param.isATK == true ? "true" : "false";
    }

    void Update()
    {
        // check
        if(Input.GetKey(KeyCode.P))
        {
            param.ATK = 5;
            Change();
        }
        // 仮なのでゴリゴリで書いてます・・
        skill1.GetComponent<Text>().text = csvDatas[0][0] ;
        skill2.GetComponent<Text>().text = csvDatas[0][1];
        skill3.GetComponent<Text>().text = csvDatas[1][0];
        skill4.GetComponent<Text>().text = param.ATK.ToString();
        /*
        skill5.GetComponent<Text>().text = csvDatas[2][0];
        skill6.GetComponent<Text>().text = csvDatas[2][1];
        */
    }
}