using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// スキルドラッグ時
public class UIEvent : MonoBehaviour
{ 
    private Vector3 screenPoint;
    private Vector3 offset;
    // 座標記憶用
    private Vector3 defaultPosition;
    // 記憶用
    private SpriteRenderer currentRenderer;
    private Sprite currentSprite;
    // マウスボタンを離したときスキルがセットされる位置かどうか
    private bool setFlag = false;
    // ドラッグ中判定
    private bool dragFlag = false;
    // UI内判定
    private bool insideFlag = false;
    // スキル変更フラグ
    private bool changeFlag = false;
    private string currentSkill = "";
    public string GetSkillName()
    {
        return currentSkill;
    }

    // ====================
    // カーソルが範囲内に入ったとき
    // ====================


    // ====================
    // カーソルが範囲外に出たとき
    // ====================


    // ===================
    // マウスクリックしたとき
    // ===================
    private GameObject prefab;
    private GameObject clone;
    private Image current;
    // アルファ値
    [SerializeField] float alpha = 0.5f;
    // クリックした場所に生成
    public void Create()
    {
        // 生成
        prefab = (GameObject)Resources.Load("Skill/Prefab/Clone");
        var mouse = Input.mousePosition;
        clone = Instantiate(prefab, new Vector3(mouse.x, mouse.y), Quaternion.identity, GameObject.Find("CloneCanvas").transform);
        // 画像を記憶
        current = GetComponent<Image>();
        // クローンの設定
        var sr = clone.GetComponent<Image>();
        sr.sprite = null;
        sr.sprite = current.sprite;
        sr.color = new Color(1, 1, 1, alpha);
        // 生成したオブジェクトの名前から(Clone)を消しておく
        clone.name = prefab.name;
    }

    // キャンセルボタン押したとき
    // グレー値
    [SerializeField] float gray = 0;
    [SerializeField] bool grayFlag = false;
    // グレーアウト
    public void GrayOut()
    {
        GetComponent<Image>().color = new Color(gray, gray, gray, 1);
        grayFlag = true;
    }

    // Rectの値
    [SerializeField] float ReduceRect = 0;
    [SerializeField] float BaseRect = 0;
    // UIクリック時の縮小
    public void Reduce()
    {
        RectTransform rt = GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(ReduceRect, 0);
    }

    // 文字用縮小
    public void ReduceText()
    {
        GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f);
    }


    // パラメータ
    public GameObject parameter;
    // スロット
    public GameObject[] slot;
    // キャンセルクリック時のスロット側パラメータ文字戻し
    public void CancelClick()
    {
        var i = 0;
        foreach (var s in slot)
        {
            var pp = parameter.GetComponent<PlayerParameter>();
            var t = pp.GetSlotName(i);
            var a = pp.GetParameterToString(t);

            if (a != null)
            {
                s.GetComponent<Text>().text = a;
            }
        }
    }

    // SEがあれば再生
    public void PlaySE()
    {
        var sound = GetComponent<AudioSource>();
        if (sound == null) return;
        sound.PlayOneShot(sound.clip);
    }


    // =================
    // マウスボタンを離したとき
    // =================
    public void Delete()
    {
        // 元に戻す
        dragFlag = false;
        // cloneを削除
        Destroy(clone);
    }

    public void DeleteThis()
    {
        Destroy(this);
    }

    // UI縮小解除
    public void Extension()
    {
        RectTransform rt = GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(BaseRect, 0);
    }

    // 文字用縮小解除
    public void ExtensionText()
    {
        GetComponent<RectTransform>().localScale = new Vector3(1, 1);
    }

    // ==============
    // ドラッグしている間
    // ==============
    // 移動
    public void Move()
    {
        // ドラッグフラグオン
        dragFlag = true;
        // マウスの座標に移動
        var mouse = Input.mousePosition;
        clone.transform.position = new Vector3(mouse.x, mouse.y);
    }

    // ====================
    // ドロップしたとき
    // ====================
    // グレーアウト解除
    public void Active()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        grayFlag = false;
    }
}
