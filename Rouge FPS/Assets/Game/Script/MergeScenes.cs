using UnityEngine;
using UnityEngine.SceneManagement;
public class MergeScenes : MonoBehaviour
{
    private static bool CameraSeter = false;

    private static bool isMerge = false;       //シーン統合フラグ

    private static bool FirstLoad=false;

    void Start()
    {
       
        FloorCount.SetReset();
         //SceneManager.LoadScene("SkillScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("MapScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
        FirstLoad=true;

        // SceneManager.LoadScene("EnemyScene", LoadSceneMode.Additive);
        // // SceneManager.LoadScene("EnemyScene", LoadSceneMode.Additive);

       
         //SceneManager.LoadScene("WeaponScene", LoadSceneMode.Additive);

        isMerge = true;

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
      
    }
    public static bool CameraSet
    {
        get { return CameraSeter; }  //取得用

    }

    //シーン統合フラグ取得
    public static bool IsMerge()
    {
        return isMerge;
    }

        public static bool IsLoad()
    {
        return FirstLoad;
    }
}