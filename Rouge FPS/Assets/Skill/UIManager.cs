using UnityEngine;
using UnityEngine.UI;

// UI管理クラス
public class UIManager : MonoBehaviour
{
    public GameObject[] SkillObj;
    [SerializeField] Vector2[] SkillPos;
    [Header("-----------------------------------------")]
    public GameObject[] SlotObj;
    [SerializeField] Vector2[] SlotPos;
    [Header("-----------------------------------------")]
    public GameObject[] ButtonObj;
    [SerializeField] Vector2[] ButtonPos;
    [Header("-----------------------------------------")]
    public GameObject UIObj;
    [Header("-----------------------------------------")]
    public GameObject Parameter;
    [Header("-----------------------------------------")]
    public GameObject TextObj;
    public Vector2 TextPos;
    [Header("-----------------------------------------")]
    public GameObject[] EtcObj;
    [Header("-----------------------------------------")]
    public GameObject GuideObj;
    [Header("-----------------------------------------")]
    public GameObject CursorObj;

    // 抽選用の変数
    private Lottery lot;

    // 現在の階層
    private string nowFloor;
    public void SetNowFloor(int _floor)
    {
        nowFloor = _floor.ToString();
    }

    // UI表示フラグ
    private static bool UIFlag = false;
    public static void SetUIFlag(bool _flag)
    {
        UIFlag = _flag;
    }
    


    //操作用フラグ
    private static bool GetUIFlg=false;

    public  static bool GetFlg(){return GetUIFlg;}

    private bool guideFlag = false;
    public void SetGuide(bool _flag)
    {
        guideFlag = _flag;
    }

    // 暗転終了フラグ
    private bool EndTransition = false;
    public void EndBlackOut()
    {
        EndTransition = true;
    }

    private GameObject TransitionObj;

    void Start()
    {
        TransitionObj = GameObject.Find("TransitionCanvas");
        lot = new Lottery();
        nowFloor = "0";
        Init();
    }
    
    // 初期化
    public void Init()
    {
        // 各フラグ初期化
        UIFlag = false;
        EndTransition = false;
        guideFlag = false;

        // 座標、パラメーター設定
        TextObj.transform.position = TextPos;
        var i = 0;
        foreach (var obj in SkillObj)
        {
            // 初期座標設定
            obj.transform.position = SkillPos[i];
            i++;
        }
        i = 0;
        // プレイヤーのパラメーター情報を取得
        var playerParam = GameObject.Find("Parameter").GetComponent<PlayerParameter>();
        foreach (var obj in SlotObj)
        {
            // 初期座標設定
            obj.transform.position = SlotPos[i];
            // スロット名設定
            obj.GetComponent<Parameter>().SetName(playerParam.GetSlotName(i));
            i++;
        }
        i = 0;
        foreach (var obj in ButtonObj)
        {
            // 初期座標設定
            obj.transform.position = ButtonPos[i];
            i++;
        }
        // UI非表示
        UIObj.SetActive(false);
    }

    // パラメーターをリセット
    public void ParameterReset()
    {
        var i = 0;
        foreach (var obj in SkillObj)
        {
            obj.GetComponent<Parameter>().AllReset();
            i++;
        }
        i = 0;
        foreach (var obj in SlotObj)
        {
            obj.GetComponent<Parameter>().AllReset();
            i++;
        }
    }

    // スキルのセット
    public void SetSkill(string _floor, params int[] _table)
    {
        var i = 0;
        foreach (var obj in SkillObj)
        {
            // 抽選
            // 第一引数はフロア、第２引数は強いスキルの出やすさ確率をint配列で渡す
            var status = lot.LevelUp(_floor, _table);
            // テクスチャ設定
            var tex = "Skill/" + status[0];
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(tex);
            // パラメーター設定
            var param = obj.GetComponent<Parameter>();
            param.AllReset();
            param.SetParameter(status[0], status[1]);
            // 名前設定
            param.SetName(status[0]);

            // スキル効果値の文字描画
            var child = obj.GetComponentInChildren<SkillValue>();
            child.SetText();

            // アニメーション再設定
            obj.GetComponent<AiryUIAnimationManager>().ShowMenu();

            i++;
        }
    }
    
    // スロットのセット
    public void SetSlot()
    {
        var i = 0;
        var pp = Parameter.GetComponent<PlayerParameter>();
        foreach(var obj in SlotObj)
        {
            // パラメーター取得
            var param = obj.GetComponent<Parameter>();
            param.AllReset();
            // i番目のスロットの情報をセット
            var name = pp.GetSlotName(i);
            var tex = "Skill/" + name;
            // テクスチャ設定
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(tex);
            // パラメーターの値も取得
            var check = pp.GetSlotValue(i);
            param.SetParameter(name, pp.GetSlotValue(i));

            // スキル効果値の文字描画
            var child = obj.GetComponentInChildren<SkillValue>();
            child.SetText();

            // ドロップ側UIの初期化設定
            obj.GetComponent<DropUI>().InitParameter();

            // アニメーション再設定
            obj.GetComponent<AiryUIAnimationManager>().ShowMenu();
            

            i++;
        }
    }

    // ボタンのセット
    public void SetButton()
    {
        foreach(var obj in ButtonObj)
        {
            obj.GetComponent<AiryUIAnimationManager>().ShowMenu();
        }
    }

    // タイトルテキストのセット
    public void SetText()
    {
        TextObj.GetComponent<AiryUIAnimationManager>().ShowMenu();
    }

    // その他UIのセット
    public void SetEtc()
    {
        foreach(var obj in EtcObj)
        {
            obj.GetComponent<AiryUIAnimationManager>().ShowMenu();
        }
    }

    // ガイド設定
    public void SetGuide()
    {
        GuideObj.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);
        GuideObj.SetActive(false);
    }

    // マウスカーソル設定
    public void SetCursor()
    {
        CursorObj.SetActive(true);
        CursorObj.GetComponent<AiryUIAnimationManager>().ShowMenu();
    }

    // 全UI一斉終了アニメーション
    public void AllHideAnimation()
    {
        guideFlag = false;
        foreach (var obj in SkillObj)
        {
            obj.GetComponent<AiryUIAnimationManager>().HideMenu();
        }
        foreach (var obj in SlotObj)
        {
            obj.GetComponent<AiryUIAnimationManager>().HideMenu();
        }
        foreach (var obj in ButtonObj)
        {
            obj.GetComponent<AiryUIAnimationManager>().HideMenu();
        }
        foreach (var obj in EtcObj)
        {
            obj.GetComponent<AiryUIAnimationManager>().HideMenu();
        }
        TextObj.GetComponent<AiryUIAnimationManager>().HideMenu();
        CursorObj.GetComponent<AiryUIAnimationManager>().HideMenu();
    }

    private void Update()
    {
        GetUIFlg=UIObj.activeSelf;
        // 仮　UI出すためのフラグ：オンにしてくれたら勝手にアニメーション始まります
        // if(Input.GetKeyDown(KeyCode.A))
        // {
        //     UIFlag = true;
        // }
        // 暗転開始
        if (UIFlag) 
        {
            GetComponent<Transition>().BeginTransition();
            // フラグオフ
            UIFlag = false;
            guideFlag = false;
        }
        // 暗転終了したらUIの設定
        if(EndTransition)
        {
            EndTransition = false;
            // 各UIの情報
            UIObj.SetActive(true);
            SetCursor();
            SetGuide();
            SetText();
            SetButton();
            SetEtc();
            SetSlot();
            SetSkill(nowFloor, 85, 10, 5);
            DropUI.UnLock();
            // 明転
            GetComponent<Transition>().EndTransition();
        }

        // ガイド表示
        if (guideFlag)
        {
            GuideObj.SetActive(true);
        }
        else
        {
            GuideObj.SetActive(false);
        }

        // 仮　UI非表示
        // if(Input.GetKeyDown(KeyCode.S))
        // {
        //     UIObj.SetActive(false);
        //    guideFlag = false;
       // }
    }
}
