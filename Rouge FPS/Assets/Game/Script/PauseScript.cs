using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    //　ポーズした時に表示するUI
    [SerializeField]
    private GameObject pauseUI;
     public GameObject FPSCon;
    private static bool abc;
    private void Start()
    {
        pauseUI.SetActive(false);   
        Time.timeScale=1f;
    }


    // Update is called once per frame
    void Update()
    {
        if(pauseUI.activeSelf){
            if(Input.GetKeyDown(KeyCode.Return)){ 
                if(GametoTitle.GetAnswer()){       //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);
            Time.timeScale=1f;
            }
            }
        }
        abc=pauseUI.activeSelf;
        if(FPSCon.GetComponent<MyStatus>().GetHp()>0){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GametoTitle.GetNoMove()){
            //　ポーズUIのアクティブ、非アクティブを切り替え
            pauseUI.SetActive(!pauseUI.activeSelf);}

            //　ポーズUIが表示されてる時は停止
            if (pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
                //　ポーズUIが表示されてなければ通常通り進行
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    }
     public static bool pause() {
        return abc;
    }
}