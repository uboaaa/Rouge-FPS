using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// プールされるUIスクリプト
public class PoolUI : PoolObject<PoolUI>
{
    public override void Init()
    {
        gameObject.SetActive(true);
    }

    public void Set(Vector3 _pos, string _name)
    {
        transform.position = _pos;
        name = _name;
    }

    public override void Sleep()
    {
        gameObject.SetActive(false);

    }
}
