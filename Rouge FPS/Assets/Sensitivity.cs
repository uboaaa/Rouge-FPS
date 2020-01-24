using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ak;
    private static float Click;
    void Start()
    {
    Click=PlayerPrefs.GetFloat("XRotate");
    if(Click<1){Click=2.0f;}
    }
     public void Onclick(){Click+=2.0f;}

     public static float PlusX(){return Click;}
    // Update is called once per frame
    private void OnDestroy() {
        PlayerPrefs.SetFloat ("XRotate", Click);
        PlayerPrefs.Save ();
    }
    void Update()
    {
    }
}
