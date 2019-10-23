using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitletoGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetKeyDown(KeyCode.Escape)) { Quit(); }
        if (Input.GetKeyDown(KeyCode.Return)){SceneManager.LoadScene("GameScene");}
    }
}
