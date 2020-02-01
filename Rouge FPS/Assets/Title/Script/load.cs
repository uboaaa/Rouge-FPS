using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    private AsyncOperation _async;


  
	IEnumerator Start () {
        yield return new WaitForSeconds(1);

  

        //非同期でロード開始
        _async = SceneManager.LoadSceneAsync("GameScene");
        
 
        //シーン移動を許可するかどうか
        _async.allowSceneActivation = false;

        while (_async.progress < 0.9f) //0.9で止まる
        {
             Debug.Log(_async.progress);
            _loadingBar.value = _async.progress;
            yield return 0;
        }
        Debug.Log("ロード完了");
        _loadingBar.value = 1.0f;
        _async.allowSceneActivation = true;

        yield return _async;
    }

    
}
