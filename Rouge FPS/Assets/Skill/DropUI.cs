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
    private Sprite defaultSprite;

    void Start()
    {
        // もともとのスプライト記憶
        defaultSprite = nowSprite = GetComponent<Image>().sprite;
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
    }

    // UIからマウスが出たとき
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (Lock) return;
        if (pointerEventData.pointerDrag == null) return;
        // １つ前のスプライトに戻す
        iconImage.sprite = nowSprite;
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
        // スプライト変更
        Image droppedImage = pointerEventData.pointerDrag.GetComponent<Image>();
        iconImage.sprite = droppedImage.sprite;
        // １つ前のスプライトも変更
        nowSprite = droppedImage.sprite;
        iconImage.color = new Color(1, 1, 1, 1);
        Lock = true;
    }

    // キャンセルボタン押されたとき
    public void CancelClick()
    {
        // 初期状態に戻す
        iconImage.sprite = nowSprite = defaultSprite;
        UnLock();
    }
}