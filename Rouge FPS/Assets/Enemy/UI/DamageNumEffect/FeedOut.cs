using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedOut : MonoBehaviour
{

    private float alpha;
    TextMesh tm;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 1.0f;

        tm = this.gameObject.GetComponent<TextMesh>();
        tm.color = new Color(1.0f,1.0f,1.0f,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        tm.color = new Color(1.0f,1.0f,1.0f,alpha);
        alpha -= 0.03f;
        if(alpha < 0)
        {
            alpha = 0.0f;
            Destroy(this.gameObject);
        }
    }
}
