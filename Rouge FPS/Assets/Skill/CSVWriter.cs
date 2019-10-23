using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    Param param;

    void Start()
    {

    }

    void Update()
    {

    }

    // 引数はファイル名、上書き保存するかどうか(trueで上書き、falseで新規作成)
    public void Write(string _pass, bool _b)
    {
        string s = "Assets/Resources/CSV/" + _pass;
        try
        {
            // 出力用のファイルを開く
            // 引数のtrueをfalseにするとファイル新規作成
            using (var sw = new System.IO.StreamWriter(s, _b))
            {
                /*
                for (int i = 0; i < x.Length; ++i)
                {
                    // 
                    sw.WriteLine(, , y[i], z[i]);
                }
                */
            }
        }
        catch (System.Exception e)
        {
            // ファイルを開くのに失敗したときエラーメッセージを表示
            System.Console.WriteLine(e.Message);
        }
    }


}
