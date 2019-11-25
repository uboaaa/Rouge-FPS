using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CameraSetting : MonoBehaviour
{
    private GameObject MainCam;
    private GameObject DirecLight;
    private bool aho=false;
    string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = GameObject.Find("Main Camera");
        DirecLight = GameObject.Find("Directional Light");
        SceneName = SceneManager.GetActiveScene().name;
        aho = MergeScenes.CameraSet;

       

        //if (SceneName == "PlayerScene" || SceneName=="EnemyScene" ||SceneName=="MapScene")
        //{

            if (aho == true)
            {
            Destroy(this.GetComponent<Camera>());
            //DirecLight.SetActive(false);
        }

        //}
    }
    // Update is called once per frame
    void Update()
    {

    }
}
