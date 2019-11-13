using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GametoTitle : MonoBehaviour
{
    private GameObject gameObject;
    private bool GoGame = false;
    private string ModeSelect="ReturnGame";

    [SerializeField]
    private GameObject RGame;
    [SerializeField]
    private GameObject RTitle;
    [SerializeField]
    private GameObject Settings;

    // ReturnGame is called before the first frame update
    void ReturnGame()
    {
    
  
    }


    // Update is called once per frame
    void Update()
    {

        Debug.Log(RTitle);


        switch (ModeSelect)
        {
            case "ReturnGame":
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "Settings"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "ReturnTitle"; }
                if (Input.GetKeyDown(KeyCode.Return)) { EnterGame(); }
                    RGame.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
                break;

            case "ReturnTitle":
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "ReturnGame"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "Settings"; }
               RTitle.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
                break;

            case "Settings":
                if (Input.GetKeyDown(KeyCode.UpArrow)) { ModeSelect = "ReturnTitle"; }
                if (Input.GetKeyDown(KeyCode.DownArrow)) { ModeSelect = "ReturnGame"; }
                Settings.GetComponent<TextColorChangeScript>().ColorChange(ModeSelect);
              
                break;
        }


    }
    public bool GetEnterGame() { return GoGame; }
    public string GetModeSelect() { return ModeSelect; }
    void EnterGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
