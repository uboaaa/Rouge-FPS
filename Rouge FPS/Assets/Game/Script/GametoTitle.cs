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
   private void Start() {
       Gett=false;
       ModeSelect="ReturnGame";
   }

    // Update is called once per frame
    void Update()
    {

  

        switch (ModeSelect)
        {
            case "ReturnGame":
                Gett=true;
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "Settings"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "ReturnTitle"; }
                if (Input.GetKeyDown(KeyCode.Return)) { EnterGame(); }
                    RGame.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
                break;

            case "ReturnTitle":
                Gett=false;
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "ReturnGame"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "Settings"; }
               RTitle.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
                  if (FadePanel.AlphaGet()>1.0f) {SceneManager.LoadScene("TitleScene");}
                break;

            case "Settings":
            Gett=false;
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "ReturnTitle"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "ReturnGame"; }
                Settings.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
              
                break;
        }


    }
    public bool GetEnterGame() { return GoGame; }
    public  static string GetModeSelect() { return ModeSelect; }
    public static bool GetAnswer(){return Gett;}
    void EnterGame()
    {
      
    }
}
