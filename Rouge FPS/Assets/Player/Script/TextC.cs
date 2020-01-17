using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextC : MonoBehaviour
{
  
 public   GameObject target1;
     Transform target;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
     
         this.GetComponent<Text> ().text = ""+target1.transform.rotation.y;
    }
}
