using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitletoGame : MonoBehaviour
{
    private GameObject gameObject;
    private bool GoGame=false;
    private string ModeSelect;

    public static bool GameCheck;

    private bool NoMove;
    private GameObject[] Mode = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("Panel");
        Mode[0] = GameObject.Find("Start");
        Mode[1] = GameObject.Find("Exit");
        ModeSelect = "Start";
        GameCheck=false;
        NoMove=false;
    }
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
       GoGame=gameObject.GetComponent<FadePanel>().GetAllBlack();
        if (Input.GetKeyDown(KeyCode.Escape)) { Quit(); }
        
        switch (ModeSelect)
        {
            case "Start":
            if(!NoMove){
                GameCheck=true;
                if (Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Exit"; }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { ModeSelect = "Exit"; }
                Mode[0].GetComponent<TextColorChange>().ColorChange(ModeSelect);
                  if (Input.GetKeyDown(KeyCode.Return)){NoMove=true;}}
                if (FadePanel.AlphaGet()>1.0f) {
                    if(!MergeScenes.IsLoad()){SceneManager.LoadScene("LoadingScene");}
                    else{SceneManager.LoadScene("GameScene");}
                    } 
                break;

            // case "Setting":
            // if(!NoMove){
            //      GameCheck=false;
            //     if (Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Exit"; }
            //     if (Input.GetKeyDown(KeyCode.LeftArrow)) { ModeSelect = "Start"; }
            //     Mode[2].GetComponent<TextColorChange>().ColorChange(ModeSelect);}
            //        if (Input.GetKeyDown(KeyCode.Return)){NoMove=true;}
            //     break;

            case "Exit":
            if(!NoMove){
                GameCheck=false;
                if (Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Start"; }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { ModeSelect = "Start"; }
                Mode[1].GetComponent<TextColorChange>().ColorChange(ModeSelect);}
                if (Input.GetKeyDown(KeyCode.Return)) {AudioManager.Instance.PlaySE("button01b"); NoMove=true; Quit(); }
                break;
        }
        

        
    }
    public bool GetEnterGame() {return GoGame;}
    public string GetModeSelect() { return ModeSelect; }
    public static bool GetGame(){return GameCheck;}
}
