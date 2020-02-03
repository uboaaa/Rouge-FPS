using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// トランジション処理
public class Transition : MonoBehaviour
{
    // マテリアル
    [SerializeField]
    private Material transition;

    // シェーダID
    private int PropID;
    // 暗転・明転タイマー
    private float time;
    bool beginFlag = false;
    public void BeginTransition()
    {
        beginFlag = true;
    }
    bool EndFlag = false;
    public void EndTransition()
    {
        EndFlag = true;
    }
    [SerializeField] float speed;


    private void Awake()
    {
        // ID取得
        PropID = Shader.PropertyToID("_Alpha");
        time = 0.0f;
        transition.SetFloat(PropID, time);
    }

    private void Update()
    {
        // 暗転開始
        if(beginFlag)
        {
            time += speed;
            if (time >= 1.0f)
            {
                time = 1.0f;
                beginFlag = false;
                // 暗転終了
                GameObject.Find("UIManager").GetComponent<UIManager>().EndBlackOut();
            }
            transition.SetFloat(PropID, time);
        }
        // 明転開始
        if(EndFlag)
        {
            time -= speed;
            if (time <= 0.0f)
            {
                time = 0.0f;
                EndFlag = false;
            }
            transition.SetFloat(PropID, time);
        }
    }
}