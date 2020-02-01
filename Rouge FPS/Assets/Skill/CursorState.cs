using UnityEngine;
using UnityEngine.UI;

public class CursorState : MonoBehaviour
{
    private Sprite sprite;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        // マウスカーソル設定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        sprite = Resources.Load<Sprite>("Skill/cursor");
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.visible)
        {
            Cursor.visible = false;
        }
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        image.sprite = sprite;
    }
}
