using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo animatorInfo;
    GunController GCScript;
    bool animFlg = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        GCScript = GetComponent<GunController>();
    }

    void Update()
    {
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        // アニメーションが終了
        if(animatorInfo.normalizedTime > 1.0f)
        {
            // 射撃
            if(Input.GetMouseButtonDown(0) && GCScript.Ammo > 0 && animFlg == false)
            {
                animator.SetBool("ShootFlg",true);
                animFlg = true;
            }

            // リロード
            if(Input.GetKeyDown(KeyCode.R) && GCScript.MaxAmmo > 0 && animFlg == false && GCScript.shootEnabled)// && GCScript.Ammo != GCScript.OneMagazine)
            {
                animator.SetBool("ReloadFlg",true);
                animFlg = true;
            }
        }
        else
        {
            animator.SetBool("ShootFlg",false);
            
            animator.SetBool("ReloadFlg",false);

            animFlg = false;
        }
    }
}
