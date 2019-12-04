﻿using System.Collections;
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

        if(StageReset){
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

            UpdatePlayerXYZ(abc.x, 15, abc.z);
        }

        // //ノックバック処理
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     abc = this.transform.position;
        //     for (int i = 0; i < 5; i++)
        //     {
        //         moveDirection.z -= 1.0f;
        //         moveFlg = true;
        //     }
        // }
        // if (moveFlg)
        // {
        //     characterController.Move(moveDirection * Time.deltaTime);
        // }
        // if (abc.z -transform.position.z>10.0f) { moveFlg = false; }
    }


//   void OnTriggerEnter (Collider other)
//     {
//         string layerName = LayerMask.LayerToName(other.gameObject.layer);
         
//         if( layerName == "Back")
//         {
//             Debug.Log("AAA");
//            moveFlg = false;
//         }
//     }
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

            case "rx":
            result=rx;
            break;

            case "ry":
            result=ry;
            break;

            case "rz":
            result=rz;
            break;

            default:
            break;
        }
        
        return result;
        
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
