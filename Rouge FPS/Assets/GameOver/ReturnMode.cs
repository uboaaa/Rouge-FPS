using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMode : MonoBehaviour
{
private GameObject gameObject;
    private bool GoGame = false;
    public static  string ModeSelect="ReStart";
    [SerializeField]
    private GameObject RGame;
    [SerializeField]
    private GameObject RTitle;

  
    private static bool Gett=false;
   private void Start() {
       Gett=false;
       ModeSelect="ReStart";
       
   }

    // Update is called once per frame
    void Update()
    {
   
      
    
        switch (ModeSelect)
        {
            case "ReStart":
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "Title"; }
                if (FadePanel.AlphaGet()>1.0f) {SceneManager.LoadScene("GameScene");}
                    RGame.GetComponent<SelectMode>().ColorChange(ModeSelect);
            
                break;

            case "Title":
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) { ModeSelect = "ReStart"; }
                if (FadePanel.AlphaGet()>1.0f) {SceneManager.LoadScene("TitleScene");}
                RTitle.GetComponent<SelectMode>().ColorChange(ModeSelect);
                
                break;

        }
        


    }
public static bool GetEnter(){return Gett;}
}

