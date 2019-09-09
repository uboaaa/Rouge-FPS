using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CameraSetting : MonoBehaviour
{
    private GameObject MainCam;
    public MergeScenes mergeScenes;
    GameObject mainCamObj;
    private bool aho=false;
    
    // Start is called before the first frame update
    void Start()
    {
        aho = MergeScenes.CameraSet;
        MainCam = GameObject.Find("Main Camera");
        //aho = mergeScenes.CameraSet();
        if (aho== true) {
            MainCam.SetActive(false);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(aho);
    }
}
