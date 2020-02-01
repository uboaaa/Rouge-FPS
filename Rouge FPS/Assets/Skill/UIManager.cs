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

    // 抽選用の変数
    private Lottery lot;

    // 現在の階層
    private string nowFloor;

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

    // スキルの画像セット
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


            i++;
        }
    }
    
    // スロットの画像セット
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

            i++;
        }
    }





    private void Update()
    {
        // 暗転開始
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            GetComponent<Transition>().BeginTransition();
        }
        // 暗転終了したら設定
        if(EndTransition)
        {
            EndTransition = false;
            // スロット、スキル設定・表示
            SetSlot();
            SetSkill(nowFloor, 5, 4, 1);
            DropUI.UnLock();
            UIObj.SetActive(true);
            // 明転
            GetComponent<Transition>().EndTransition();
        }

        // UI非表示
        if(Input.GetKeyDown(KeyCode.S))
        {
            UIObj.SetActive(false);
        }
    }
}
