using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorAllBlack : MonoBehaviour
{
    
    private Text GameText;
  
    private Text TitleText;

    private Text SettingsText;

    [SerializeField]
    private GameObject RGame;
    [SerializeField]
    private GameObject RTitle;


    [SerializeField]
    private GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
         TitleText=RTitle.GetComponent<Text>();
        GameText=RGame.GetComponent<Text>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!pauseUI.activeSelf){
            TitleText.color=Color.black;
            GameText.color=Color.black;
 
        }
        
    }
}
