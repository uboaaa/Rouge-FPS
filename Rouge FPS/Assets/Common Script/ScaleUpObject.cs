using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SkillManagement.GetBulletScaleUp()){
       this.transform.localScale = new Vector3(5, 5, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
