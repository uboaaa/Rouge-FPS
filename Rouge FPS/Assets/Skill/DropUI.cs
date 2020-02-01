using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 複数スキル変更をやめる
    static bool Lock = false;
    static public void UnLock()
    {
        Lock = false;
    }
    // 変更するUI
    public Image iconImage;
    // 今描画しているスプライト
    private Sprite nowSprite;
    // もともとのスプライト記憶用
    private Sprite memorySprite;
    private string memoryName;
    // 直前のスキル
    private string currentSkillValue;
    private string currentSkillName;

    // ドロップ時の文字描画用変数
    // テキスト
    public GameObject textObj;


    void Start()
    {
        InitSprite();
        InitParameter();
    }

    // スプライト初期設定
    public void InitSprite()
    {
        // もともとのスプライト記憶
        memorySprite = nowSprite = GetComponent<Image>().sprite;
        memoryName = memorySprite.name;
    }

    public void InitParameter()
    {
        // キャンセル時もとに戻すため記憶
        var nowParam = GetComponent<Parameter>();
        currentSkillName = nowParam.GetName();
        currentSkillValue = nowParam.GetParameter(currentSkillName);
    }


    // UIにマウスがはいったとき
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // 早期リターン
        if (Lock) return;
        if (pointerEventData.pointerDrag == null) return;
        // マウスで掴んでいるUIの画像に変更
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        iconImage.color = new Color(1, 1, 1, 0.5f);
        // 文字描画オフ
        textObj.GetComponent<Text>().color = new Color(1, 1, 1, 0);
    }

    // UIからマウスが出たとき
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (Lock) return;
        if (pointerEventData.pointerDrag == null) return;
        // １つ前のスプライトに戻す
        iconImage.sprite = nowSprite;

        textObj.GetComponent<Text>().color = new Color(1, 1, 1, 1);

        // 掴んでいるスプライトがあるかどうか
        if (nowSprite == null)
        {
            iconImage.color = Vector4.zero;
        }
        else
        {
            iconImage.color = Vector4.one;
        }
    }

    // UI内でドロップしたとき
    public void OnDrop(PointerEventData pointerEventData)
    {
        if (Lock) return;
        Lock = true;

        // パラメーター変更
        // キャンセル時もとに戻すため記憶
        var nowParam = GetComponent<Parameter>();
        currentSkillName = nowParam.GetName();
        currentSkillValue = nowParam.GetParameter(currentSkillName);

        // ドラッグしてきたスキルのパラメーター
        var param = pointerEventData.pointerDrag.GetComponent<Parameter>();
        // その名前
        var name = param.GetName();
        //  スロットの名前をスキルの名前に変更
        nowParam.SetName(name);
        // スロットの値を書き換える
        nowParam.AllReset();
        nowParam.SetParameter(name, param.GetParameter(name));

        // 描画している効果値変更
        textObj.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        textObj.GetComponent<SkillValue>().SetText();

        // スプライト変更
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        // １つ前のスプライトも変更
        nowSprite = droppedImage.sprite;
        iconImage.color = new Color(1, 1, 1, 1);

        // ドロップ時のSE再生
        var sound = GetComponent<AudioSource>();
        if (sound)
        {
            sound.PlayOneShot(sound.clip);
        }
    }


    // キャンセルボタン押されたとき
    public void CancelClick()
    {
        // 初期状態に戻す
        // 描画している画像を戻す
        iconImage.sprite = nowSprite = memorySprite;
        // パラメーターも元に戻す
        var nowParam = GetComponent<Parameter>();
        nowParam.AllReset();
        nowParam.SetName(currentSkillName);
        nowParam.SetParameter(currentSkillName, currentSkillValue);

        // 描画している効果値変更
        textObj.GetComponent<SkillValue>().SetText();

        // 切り替え可能状態に戻す
        UnLock();
    }
}