using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
     [SerializeField]
    private GameObject UI;

    [SerializeField]
    private GameObject Caaa;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   public void OnClick()
    {
       UI.GetComponent<SettingScript>().OffPause();
       UI.GetComponent<PauseScript>().OnPause();
       Rotation.Save();
       Caaa.GetComponent<Came>().Save();
    }
}
