using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerXYZ : MonoBehaviour
{
    private bool ClearCheckFlg=true;
    Vector3 abc = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    bool moveFlg = false;

    private static float px;
    private static float py;
    private static float pz;

    private static float rx;
    private static float ry;
    private static float rz;

    private static bool StageReset;

    private static Quaternion rotation;

private float aaaaa;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        StageReset=true;

    }

    // Update is called once per frame
    void Update()
    {
        rotation = this.transform.localRotation;
      

        if(StageReset || MapInitializer.GetSpawnEnable()){
        FloorCount.UpFloors();
        characterController = GetComponent<CharacterController>();
        characterController.enabled = false;
        abc.x = MapInitializer.GetSpawnData("px");
        abc.y = MapInitializer.GetSpawnData("py") ;
        abc.z = MapInitializer.GetSpawnData("pz");
        UpdatePlayerXYZ(abc.x, abc.y, abc.z);}
        px=this.transform.position.x;
        py=this.transform.position.y;
        pz=this.transform.position.z;
        rx=this.transform.rotation.x;
        ry=this.transform.rotation.y;
        rz=this.transform.rotation.z;
       
        //リセット処理その２(その１はFirstPersonController.cs)
        // if (Input.GetKey(KeyCode.Return)) {
        //     characterController.enabled = false;
        //     ClearCheckFlg = true; }

        if (ClearCheckFlg) {

            // UpdatePlayerXYZ(abc.x, 15, abc.z);
        }

        
    }



        void FixedUpdate()
        {
       

    }


    public bool GetClearCheckFlg() { return ClearCheckFlg; }

    public static void SetStageResetFlg(bool Judge){StageReset=Judge;}

    public static float GetPlayerPosition(string element){
        float result=0.0f;
        switch (element)
        {
            case "px":
            result=px;
            break;

            case "py":
            result=py;
            break;

            case "pz":
            result=pz;
            break;

            default:
            break;
        }
        
        return result;
        
    }

    public static Vector3 GetPlayerRotation(){
     // クォータニオン → オイラー角への変換
    Vector3 rotationAngles = rotation.eulerAngles;
    return rotationAngles;
    }

    public void UpdatePlayerXYZ(float x, float y ,float z){
        //this.transform.rotation = Quaternion.identity;
        transform.Reset();
        transform.Reset(isLocal: true);
        Vector3 XYZ = new Vector3(x, y, z);
        transform.Translate(XYZ);
        GetComponent<CharacterController>().enabled = true;
        ClearCheckFlg = false;
        StageReset=false;
        //GetComponent<Rigidbody>().position = XYZ;
    }

  
}
