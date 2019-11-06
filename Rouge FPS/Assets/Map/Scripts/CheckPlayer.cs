using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

//確認用
//***移動遅いし、カメラおかしい、マウス使えない

public class CheckPlayer : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb = null;

    public Transform playerTransform = null;
    public Transform cameraTransform = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float x = MapInitializer.GetSpawnData("px");
        float y = MapInitializer.GetSpawnData("py");
        float z = MapInitializer.GetSpawnData("pz");
        
        playerTransform.position = new Vector3(x, y, z);
    }
    void Update()
    {
        // マウスで視点移動
        float x_rotate = Input.GetAxis("Mouse X") * 3.0f;
        float y_rotate = Input.GetAxis("Mouse Y");
        playerTransform.transform.Rotate(0, x_rotate, 0);
        cameraTransform.transform.Rotate(-y_rotate, 0, 0);

        // WASDで移動する
        float x = 0.0f;
        float z = 0.0f;

        if (Input.GetKey(KeyCode.D))
        {
            x += 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            z += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1.0f;
        }

        Vector3 move = z * playerTransform.forward + x * playerTransform.right;
        rb.velocity = move * speed;
    }
}
