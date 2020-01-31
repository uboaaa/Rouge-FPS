using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        switch(SceneManager.GetActiveScene().name){
            case "TitleScene": AudioManager.Instance.PlayBGM("bgm_maoudamashii_fantasy14");break;
            case "GameScene": AudioManager.Instance.PlayBGM("苔の洞窟");break;
            case "GameOverScene": AudioManager.Instance.PlayBGM("bgm_maoudamashii_orchestra26");break;
            default :break;
        }
       
    }
}
