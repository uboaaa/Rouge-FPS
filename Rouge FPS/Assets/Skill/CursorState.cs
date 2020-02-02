using UnityEngine;
using UnityEngine.UI;

public class CursorState : MonoBehaviour
{
    private Sprite sprite;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        sprite = Resources.Load<Sprite>("Skill/cursor");
        image = GetComponent<Image>();
        image.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // 常にマウスの座標に更新
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
}
