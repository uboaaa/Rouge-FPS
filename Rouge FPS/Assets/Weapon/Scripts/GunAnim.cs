using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnim : MonoBehaviour
{
	private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent <Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        //==========================
        // 機能性が悪いので調整予定
        //==========================
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("ShotFlg",true);
        }
        if(Input.GetMouseButtonUp(0))
        {
            animator.SetBool("ShotFlg",false);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("ReloadFlg",true);
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            animator.SetBool("ReloadFlg",false);
        }
    }
}
