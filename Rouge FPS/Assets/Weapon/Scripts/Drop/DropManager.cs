using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] GameObject　BulletPrefab;
    [SerializeField] GameObject　LifePrefab;
    void Start()
    {
        int random =　UnityEngine.Random.Range(0, 1);
        if(random == 0)
        {
            GameObject bullet = Instantiate<GameObject>(BulletPrefab);
            bullet.transform.parent = this.transform;
        } else if(random == 1) {
            GameObject life = Instantiate<GameObject>(LifePrefab);
            life.transform.parent = this.transform;
        }
    }

    void Update()
    {
        
    }
}
