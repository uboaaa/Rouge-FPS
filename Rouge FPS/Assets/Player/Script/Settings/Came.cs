using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Came: MonoBehaviour
{
     Camera camera;
     

     bool change;
    // Start is called before the first frame update
    void Start()
    {

        camera = this.GetComponent<Camera>();
        camera.fieldOfView = PlayerPrefs.GetFloat("FOV",120.0f);
        change=false;
    }

    // Update is called once per frame
    void Update()
    {

    if(ChangeCamera.GetUpPool()&& camera.fieldOfView<179){  camera.fieldOfView =camera.fieldOfView+1;ChangeCamera.FalseUpPool();}
    if(ChangeCamera.GetDownPool() &&camera.fieldOfView>1){ camera.fieldOfView-=1.0f;ChangeCamera.FalseDownPool();}

    }
    
    public float GetNumber(){return camera.fieldOfView;}
    public  void Save(){ 
    PlayerPrefs.SetFloat ("FOV", camera.fieldOfView);
    PlayerPrefs.Save ();}
}
