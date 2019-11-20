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
    private GameObject[] Mode = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("Panel");
        Mode[0] = GameObject.Find("Start");
        Mode[1] = GameObject.Find("Exit");
        Mode[2] = GameObject.Find("Setting");
        ModeSelect = "Start";
        GameCheck=false;
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
                GameCheck=true;
                if (Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Setting"; }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { ModeSelect = "Exit"; }
                  if (FadePanel.AlphaGet()>1.0f) {SceneManager.LoadScene("GameScene");}
                Mode[0].GetComponent<TextColorChange>().ColorChange(ModeSelect);
                break;

            case "Setting":
                 GameCheck=false;
                if (Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Exit"; }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { ModeSelect = "Start"; }
                Mode[2].GetComponent<TextColorChange>().ColorChange(ModeSelect);
                break;

            case "Exit":
                GameCheck=false;
                if (Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Start"; }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { ModeSelect = "Setting"; }
                Mode[1].GetComponent<TextColorChange>().ColorChange(ModeSelect);
                if (Input.GetKeyDown(KeyCode.Return)) { Quit(); }
                break;
        }

        
    }
    public bool GetEnterGame() {return GoGame;}
    public string GetModeSelect() { return ModeSelect; }
    public static bool GetGame(){return GameCheck;}
}
