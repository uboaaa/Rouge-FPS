using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCamera: MonoBehaviour
{
       
     private  Camera camera;
     
 
     
    // Start is called before the first frame update
    void Start()
    {
      
         camera = this.GetComponent<Camera>();
        // camera.fieldOfView = PlayerPrefs.GetFloat("FOV",60.0f)-30.0f;
        camera.fieldOfView=60.0f;
       
    }

    // Update is called once per frame
    void Update()
    {
    //  camera.fieldOfView = PlayerPrefs.GetFloat("FOV",60.0f)-30.0f;
    // if(ChangeCamera.GetUpPool()&& camera.fieldOfView<179){  camera.fieldOfView =(camera.fieldOfView-30.0f)+1;ChangeCamera.FalseUpPool();}
    // if(ChangeCamera.GetDownPool() &&camera.fieldOfView>1){ camera.fieldOfView=(camera.fieldOfView-30.0f)-1;ChangeCamera.FalseDownPool();}

    }
    
    // public float GetNumber(){return camera.fieldOfView;}
    // public  void Save(){ 
    // PlayerPrefs.SetFloat ("FOV", camera.fieldOfView);
    // PlayerPrefs.Save ();}
}
