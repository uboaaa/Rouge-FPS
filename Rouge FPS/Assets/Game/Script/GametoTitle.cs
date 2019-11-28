using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GametoTitle : MonoBehaviour
{
    private GameObject gameObject;
    private bool GoGame = false;
    public static  string ModeSelect="ReturnGame";
    public static bool Gett=false;

    [SerializeField]
    private GameObject RGame;
    [SerializeField]
    private GameObject RTitle;
    [SerializeField]
    private GameObject Settings;

        [SerializeField]
    private GameObject pauseUI;

    private static bool NoMove=false;
   private void Start() {
       Gett=false;
       ModeSelect="ReturnGame";
       NoMove=false;
   }

    // Update is called once per frame
    void Update()
    {
   
      if(!pauseUI.activeSelf){ModeSelect = "ReturnGame";} 
    
        switch (ModeSelect)
        {
            case "ReturnGame":
            if(pauseUI.activeSelf){
             
                Gett=true;
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "Settings"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "ReturnTitle"; }
                if (Input.GetKeyDown(KeyCode.Return)) { EnterGame();}
                    RGame.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
            }
            
                break;

            case "ReturnTitle":
             if(!NoMove){
                Gett=false;
                  if(pauseUI.activeSelf){
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "ReturnGame"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "Settings"; }
               RTitle.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
                   if (Input.GetKeyDown(KeyCode.Return)) { NoMove=true;}
                   }
                  }
                   if (FadePanel.AlphaGet()>1.0f) {SceneManager.LoadScene("TitleScene");}
                break;

            case "Settings":
              if(pauseUI.activeSelf){
            Gett=false;
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "ReturnTitle"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "ReturnGame"; }
                Settings.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
              }
              
                break;
        }
        


    }
    public bool GetEnterGame() { return GoGame; }
    public  static string GetModeSelect() { return ModeSelect; }
    public  static void SetModeSelect(string Mode) { ModeSelect=Mode; }
    public static bool GetAnswer(){return Gett;}

    public static bool GetNoMove(){return NoMove;}
    void EnterGame()
    {
      
    }
}
