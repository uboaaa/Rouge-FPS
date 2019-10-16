using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXYZ : MonoBehaviour
{
    private bool ClearCheckFlg=true;
    private Rigidbody rigidbody;
    private GameObject gameObject;
    Vector3 abc = new Vector3(0.0f, 0.0f, 0.0f);
    float aaa = 0.0f;
    int i = 0;
    bool isFirst = true; // 最初の一回を判定するフラグ
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("FPSController");
       rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得

    }

    // Update is called once per frame
    void Update()
    {

        //リセット処理その２(その１はFirstPersonController.cs)
        if (Input.GetKey(KeyCode.Return)) {
            GetComponent<CharacterController>().enabled = false;
            ClearCheckFlg = true; }

        if (ClearCheckFlg) {

            UpdatePlayerXYZ(6.0f, 0.0f, 15.0f);

        }
 
    }

        void FixedUpdate()
        {
       
            Vector3 force = new Vector3(0.0f, 100.0f, 1.0f);    // 力を設定
            rb.AddForce(force);  // 力を加える
        }

    


    public void UpdatePlayerXYZ(float x, float y ,float z){
        //this.transform.rotation = Quaternion.identity;
        transform.Reset();
        transform.Reset(isLocal: true);
        Vector3 XYZ = new Vector3(x, y, z);
        transform.Translate(XYZ);
        GetComponent<CharacterController>().enabled = true;
        ClearCheckFlg = false;
        //GetComponent<Rigidbody>().position = XYZ;
    }

    public void DamagePlayer()
    {


        

    }
}
