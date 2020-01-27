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
      Floors=1;  
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public static int GetFloors(){return Floors;}

    public static void UpFloors(){Floors++;}

    public static void SetReset(){Reset=true;}

}
