using UnityEngine;
using System.Collections;
 
public class Blinker : MonoBehaviour 
{
    [SerializeField]private float startTime = 0;    // 点滅が始まるタイミング
    [SerializeField]private float DestroyTime = 5.0f;    // 点滅が終わって消えるタイミング
	[SerializeField]private float interval = 1.0f;	// 点滅周期
    private float nextTime;
    private float endTime;

    SpriteRenderer SRScript;
 
	void Start() 
    {
		nextTime = Time.time;

        SRScript = GetComponent<SpriteRenderer>();
	}
 
	void Update() 
    {
        if(Time.time > startTime)
        {
            endTime = Time.time;
            if ( Time.time > nextTime )
            {
                SRScript.enabled = !SRScript.enabled;
 
			    nextTime += interval;
		    }
        }

        if(endTime > DestroyTime)
        {
            Destroy(gameObject);
        }
	}
}