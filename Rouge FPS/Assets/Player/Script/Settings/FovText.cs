using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FovText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject aaaak;
    Text text;
    void Start()
    {
       text=GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        text.text=""+aaaak.GetComponent<Came>().GetNumber();
    }
}
