using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace InputKey{
    /// <summary>
    /// 同時入力を禁止する
    /// </summary>
    public static class MyInput 
    {
        static bool isCheck_Input;

       public static bool MyInputKeyDown(KeyCode key)
        {
            if(Input.anyKeyDown == false) isCheck_Input = false;

            if (isCheck_Input==false)
            {
                if (Input.GetKeyDown(key))
                {
                    isCheck_Input = true;
                    return true;
                }
            }
            return false;
        }
    }
}
