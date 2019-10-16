using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSetting : MonoBehaviour
{
    private GameObject Light;
    public MergeScenes mergeScenes;
    GameObject mainCamObj;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        active = MergeScenes.CameraSet;
        Light = this.gameObject;
        //aho = mergeScenes.CameraSet();
        if (active == true)
        {
            Light.SetActive(false);

        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(active);
    }
}
