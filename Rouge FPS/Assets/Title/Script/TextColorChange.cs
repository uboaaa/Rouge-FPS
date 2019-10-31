using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour
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
        text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        switch (Search)
        {
            case "Start":
                if (text.text == "Start"){text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);}
                break;
            case "Setting":
                if (text.text == "Setting") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;
            case "Exit":
                if (text.text == "Exit") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;

        }

    }
}
