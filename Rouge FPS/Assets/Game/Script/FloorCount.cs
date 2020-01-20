using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorCount : MonoBehaviour
{
    private static int Floors;
    private static bool Reset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(SceneManager.GetActiveScene().name=="GameScene" && Reset){Floors=0;Reset=false;} 
    }

    public static int GetFloors(){return Floors;}

    public static void UpFloors(){Floors++;}

    public static void SetReset(){Reset=true;}

}
