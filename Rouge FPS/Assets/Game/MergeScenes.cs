using UnityEngine;
using UnityEngine.SceneManagement;
public class MergeScenes : MonoBehaviour
{

    void Start()
    {
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("MapScene", LoadSceneMode.Additive);


        SceneManager.LoadScene("EnemyScene", LoadSceneMode.Additive);


        SceneManager.LoadScene("SkillScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("WeaponScene", LoadSceneMode.Additive);
    }
}