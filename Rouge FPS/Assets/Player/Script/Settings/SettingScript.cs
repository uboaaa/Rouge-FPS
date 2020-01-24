using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScript : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsUI;

     private static bool abc;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OffPause(){ settingsUI.SetActive(false);  }
    public void OnPause(){ settingsUI.SetActive(true);  }
    // Update is called once per frame
    void Update()
    {
        // カーソル表示
          
          abc=settingsUI.activeSelf;
         if(settingsUI.activeSelf){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if(Input.GetKeyDown(KeyCode.Return)){ 
                if(GametoTitle.GetAnswer()){       //　ポーズUIのアクティブ、非アクティブを切り替え
            settingsUI.SetActive(!settingsUI.activeSelf);
            Time.timeScale=1f;}
            }
        }
        // else{Cursor.visible = false;
        //     Cursor.lockState = CursorLockMode.Locked;}
    
        if (Input.GetKeyDown(KeyCode.Return) && PauseScript.pause() &&GametoTitle.GetModeSelect()=="Settings")
        {
          
            //　ポーズUIのアクティブ、非アクティブを切り替え
            settingsUI.SetActive(false);
            this.GetComponent<PauseScript>().OnPause();
           
            //　ポーズUIが表示されてる時は停止
            if (settingsUI.activeSelf)
            {
                Time.timeScale = 0f;
                //　ポーズUIが表示されてなければ通常通り進行
            }

        }
    
    }
     public static bool Settingpause() {
        return abc;
    }
}
