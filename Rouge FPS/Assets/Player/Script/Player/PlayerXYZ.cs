using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXYZ : MonoBehaviour
{
    private bool ClearCheckFlg=true;
    private Rigidbody rigidbody;
    Vector3 tmp;
    // Start is called before the first frame update
    void Start()
    {
    


    }

    // Update is called once per frame
    void Update()
    {
        //リセット処理その２(その１はFirstPersonController.cs)
        if (Input.GetKey(KeyCode.Return)) {
            GetComponent<CharacterController>().enabled = false;
            ClearCheckFlg = true; }

        if (ClearCheckFlg) {

            UpdatePlayerXYZ(6.0f, 0.0f, 10.0f);

        }
 
    }


   public void UpdatePlayerXYZ(float x, float y ,float z){
        this.transform.rotation = Quaternion.identity;
        transform.Reset();
        transform.Reset(isLocal: true);
        Vector3 XYZ = new Vector3(x, y, z);
        transform.Translate(XYZ);
        tmp = GameObject.Find("FPSController").transform.position;
        GetComponent<CharacterController>().enabled = true;
        ClearCheckFlg = false;
        //GetComponent<Rigidbody>().position = XYZ;
    }
}
