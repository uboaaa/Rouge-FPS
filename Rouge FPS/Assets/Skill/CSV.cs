using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class CSV : MonoBehaviour
{
    // CSVデータ保存用
    private List<string[]> SkillDatas;
    private List<string[]> SaveDatas;

    void Awake()
    {
        SkillDatas = new List<string[]>();
        SaveDatas = new List<string[]>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // CSVデータ取得
    public List<string[]> GetSkillDatas()
    {
        return SkillDatas;
    }

    // CSV読み込み
    // 引数は格納するリストとCSVファイル名
    public void Read(ref List<string[]> _list, string _pass)
    {
        // 拡張子チェック
        string fn = _pass.Substring(_pass.Length - 4) == ".csv" ? _pass : _pass + ".csv";
        string s = "Assets/Resources/CSV/" + fn;
        // UTF-8指定
        var encoding = Encoding.GetEncoding("UTF-8");
        // すべて読み込み
        string[] lines = File.ReadAllLines(s, encoding);
        // 文字列をカンマ区切りで分割、Listに格納
        foreach (string line in lines)
        {
            string[] cells = line.Split(',');
            _list.Add(cells);
        }
    }

    // CSV書き込み　
    // 引数は読み込むリスト、CSVファイル名、上書き保存(trueで上書き、デフォルトで上書きです)
    public void Write(ref List<string[]> _list, string _pass, bool _b = true)
    {
        // 拡張子チェック
        string fn = _pass.Substring(_pass.Length - 4) == ".csv" ? _pass : _pass + ".csv";
        fn = "Assets/Resources/CSV/" + fn;
        // 書き込み用変数
        var s = new List<string>();
        foreach (var list in _list)
        {
            // カンマ区切りで追加
            s.Add(string.Join(",", list));
        }
        // 書き込み
        File.WriteAllLines(fn, s, Encoding.GetEncoding("UTF-8"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
