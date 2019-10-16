using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo animatorInfo;
    GunController GCScript;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        GCScript = GetComponent<GunController>();
    }

    void Update()
    {
        /*
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        // アニメーションが終了
        if(animatorInfo.normalizedTime > 1.0f){}
        */

        if(Input.GetMouseButtonDown(0) && GCScript.Ammo > 0)
        {
            animator.SetBool("ShootFlg",true);
        }
        if(Input.GetMouseButtonUp(0))
        {
            animator.SetBool("ShootFlg",false);
        }

        if(Input.GetKeyDown(KeyCode.R) && GCScript.MaxAmmo > 0)// && Ammo != OneMagazine)
        {
            animator.SetBool("ReloadFlg",true);
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            animator.SetBool("ReloadFlg",false);
        }
    }
}
