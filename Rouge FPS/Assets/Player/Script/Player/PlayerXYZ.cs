using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXYZ : MonoBehaviour
{
    private bool ClearCheckFlg=false;
    private Rigidbody rigidbody;
    Vector3 tmp;
    // Start is called before the first frame update
    void Start()
    {
    


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return)) {
            GetComponent<CharacterController>().enabled = false;
            ClearCheckFlg = false; }

        if (!ClearCheckFlg) {
            this.transform.rotation = Quaternion.identity;
            transform.Reset();
            transform.Reset(isLocal:true);
            UpdatePlayerXYZ(6.0f, 0.0f, 10.0f);
            tmp = GameObject.Find("FPSController").transform.position;
            GetComponent<CharacterController>().enabled = true;
            ClearCheckFlg = true;
        }
 
    }


   public void UpdatePlayerXYZ(float x, float y ,float z){
       
        Vector3 XYZ = new Vector3(x, y, z);
        transform.Translate(XYZ);
        //GetComponent<Rigidbody>().position = XYZ;
}
}
