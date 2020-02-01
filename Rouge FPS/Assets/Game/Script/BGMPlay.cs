using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMPlay : MonoBehaviour
{
    string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneName=SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {  
        Debug.Log(SceneName +":"+SceneManager.GetActiveScene().name);
        if(SceneManager.GetActiveScene().name==SceneName){
        switch(SceneManager.GetActiveScene().name){
            case "TitleScene": AudioManager.Instance.PlayBGM("Different_Dimension");break;
            case "GameScene": 
            if(FloorCount.GetFloors()%5==0){AudioManager.Instance.PlayBGM("n100");}
            else{AudioManager.Instance.PlayBGM("苔の洞窟");}break;
            case "GameOverScene": AudioManager.Instance.PlayBGM("Brutal_Nightmare");break;
            default :break;
        }
        }
    }
}
