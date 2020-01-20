using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextChange : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text=this.GetComponent<Text>();
        text.text="You Have Reached "+FloorCount.GetFloors()+" Floors";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
