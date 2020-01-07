using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMode : MonoBehaviour
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
            case "ReStart":
                if (text.text == "ReStart") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;
            case "Title":
                if (text.text == "Title") { text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f); }
                break;

        }

    }
}
