using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
   
    private CharacterController characterController;
    //　速度
    private Vector3 velocity;
    //　ジャンプ力
    [SerializeField]
    private float jumpPower = 5f;

    void Start()
    {

        characterController = GetComponent<CharacterController>();
        velocity = Vector3.zero;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            velocity = Vector3.zero;
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //　方向キーが多少押されている
            if (input.magnitude > 0f)
            {
          
                transform.LookAt(transform.position + input);
                velocity += input.normalized * 2;
                //　キーの押しが小さすぎる場合は移動しない
            }
            else
            {
    
            }
            //　ジャンプキー（デフォルトではSpace）を押したらY軸方向の速度にジャンプ力を足す
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y += jumpPower;
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}