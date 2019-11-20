using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChangeScript : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;

    void Start()
    {

        // Textコンポーネントを取得
        text = this.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void ColorChange(string Search)
    {
        text.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        switch (Search)
        {
            case "ReturnGame":
                if (text.text == "Return Game") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;
            case "ReturnTitle":
                if (text.text == "Return Title") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;
            case "Settings":
                if (text.text == "Settings") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;

        }

    }
}
