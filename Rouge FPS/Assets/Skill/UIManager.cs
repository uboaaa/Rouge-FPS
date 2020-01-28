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

    void Start()
    {
        lot = new Lottery();

        Init();
        UIObj.SetActive(false);
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
    public void SetSkill()
    {
        var i = 0;
        foreach (var obj in SkillObj)
        {
            // 抽選
            var status = lot.LevelUp("0", 4, 3, 3);
            // テクスチャ設定
            var tex = "Skill/" + status[0];
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(tex);
            // パラメーター設定
            var param = obj.GetComponent<Parameter>();
            param.AllReset();
            param.SetParameter(status[0], status[1]);
            // 名前設定
            param.SetName(status[0]);

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
            param.SetParameter(name, pp.GetParameterToString(name));
            i++;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetSkill();
            SetSlot();
            DropUI.UnLock();
            UIObj.SetActive(true);
        }
        
        if(Input.GetKeyDown(KeyCode.S))
        {

            UIObj.SetActive(false);
        }
    }
}
