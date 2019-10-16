using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CameraSetting : MonoBehaviour
{
    private GameObject MainCam;
    private GameObject DirecLight;
    private GameObject PFC;
    private bool aho=false;
    string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;
        aho = MergeScenes.CameraSet;
        MainCam = GameObject.Find("CommonFPSController");
        DirecLight = GameObject.Find("Directional Light");
       
        //aho = mergeScenes.CameraSet();
        if (SceneName == "PlayerScene" || SceneName=="EnemyScene" ||SceneName=="MapScene")
        {

            if (aho == true)
            {
                MainCam.SetActive(false);
                DirecLight.SetActive(false);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(aho);
    }
}
