using UnityEngine;
using UnityEngine.SceneManagement;
public class MergeScenes : MonoBehaviour
{
    private static bool CameraSeter = false;

 

    void Start()
    {
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("MapScene", LoadSceneMode.Additive);



        SceneManager.LoadScene("EnemyScene", LoadSceneMode.Additive);


        //SceneManager.LoadScene("SkillScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("WeaponScene", LoadSceneMode.Additive);

        //現在読み込まれているシーン数だけループ
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            //読み込まれているシーンを取得し、その名前をログに表示
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name;
          
            if (sceneName == "GameScene")
            {
                CameraSeter = true;
                break;
            }
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene("TitleScene"); }
    }
    public static bool CameraSet
    {
        get { return CameraSeter; }  //取得用

    }
}