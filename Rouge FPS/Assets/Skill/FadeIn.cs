using UnityEngine;

// UIのフェードイン
public class FadeIn : MonoBehaviour
{
    // 各UIの目標地点
    [SerializeField] float targetPosX = 0;
    [SerializeField] float targetPosY = 0;
    // フェードイン開始位置
    [SerializeField] float initPos = 0;
    // 各UIの初期位置
    float UIPosX;
    float UIPosY;
    // フェードイン時上昇量・減速・最低速度
    [SerializeField] float upDistance = 0;
    [SerializeField] float deceleration = 0;
    [SerializeField] float minSpeed = 0;
    // 減速開始位置
    [SerializeField] float downPos = 0;
    // フェードイン開始
    [SerializeField] bool IsFadein = false;
    // フェードイン終了
    bool endFlag = false;
    // 減速開始
    bool decelerationFlag = false;
    // 減速用の変数
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 初期値設定
        UIPosX = targetPosX;
        UIPosY = targetPosY + initPos;
        transform.position = new Vector3(UIPosX, UIPosY);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.U))
        {
            IsFadein = true;
            rb.velocity = new Vector2(0, upDistance);
        }

        // フェードイン開始
        if (IsFadein)
        {
            // UI座標に近づいたら減速
            if (DecelerationCheck())
            {
                // 減速
                Brake();
            }

            // 定位置まで上がったら
            if (transform.position.y >= targetPosY)
            {
                // UI位置調整
                transform.position = new Vector3(targetPosX, targetPosY);
                // フェードイン終了
                IsFadein = false;
                endFlag = true;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    // 減速開始関数
    private bool DecelerationCheck()
    {
        // 減速開始フラグチェック済み
        if (decelerationFlag) return true;
        // 減速開始位置チェック
        if (targetPosY - transform.position.y < downPos)
        {
            decelerationFlag = true;
            return true;
        }
        return false;
    }

    // 減速まとめた関数
    private void Brake()
    {
        // 減速
        rb.velocity = rb.velocity * deceleration;
        if(rb.velocity.y < minSpeed)
        {
            rb.velocity = new Vector2(0, minSpeed);
        }
        /*
        // マイナスチェック
        if (rb.velocity.magnitude - deceleration >= 0)
        {
            rb.velocity = (rb.velocity.magnitude - deceleration) * rb.velocity.normalized;
        }
        */
    }
}
