using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSettings : MonoBehaviour
{

    bool Fullscreen=false;
    // Start is called before the first frame update
    void Start()
    {
     
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12)){
            if(!Fullscreen){Fullscreen=true;Screen.SetResolution(1280, 720, Fullscreen,60);}
            else{Fullscreen=false;Screen.SetResolution(1280, 720, Fullscreen,60);}
        }
        
    }
}
