using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitletoGame : MonoBehaviour
{
    private GameObject gameObject;
    private bool GoGame=true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("Panel");
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
     
            if (GoGame) { EnterGame(); }
        
    }

    void EnterGame() {
        SceneManager.LoadScene("GameScene");
    }
}
