using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXYZ : MonoBehaviour
{
    private bool ClearCheckFlg=true;
    Vector3 abc = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    bool moveFlg = false;

    // Start is called before the first frame update
    void Start()
    {
    
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        //リセット処理その２(その１はFirstPersonController.cs)
        if (Input.GetKey(KeyCode.Return)) {
            characterController.enabled = false;
            ClearCheckFlg = true; }

        if (ClearCheckFlg) {

            UpdatePlayerXYZ(6.0f, 0.0f, 15.0f);
        }

        //ノックバック処理
        if (Input.GetKeyDown(KeyCode.C))
        {
            abc = this.transform.position;
            for (int i = 0; i < 5; i++)
            {
                moveDirection.z -= 1.0f;
                moveFlg = true;
            }
        }
        if (moveFlg)
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }
        if (abc.z -transform.position.z>10.0f) { moveFlg = false; }
    }

        void FixedUpdate()
        {
       

    }


    public bool GetClearCheckFlg() { return ClearCheckFlg; }

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

  
}
