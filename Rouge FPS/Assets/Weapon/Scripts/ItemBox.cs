using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] GameObject ItemPrefab;
    void Start(){}
    void Update(){}

    public void Open()
    {
        // 中身を表示
        Instantiate(ItemPrefab,this.transform.position,Quaternion.identity);

        //表示するときの演出
        
    }
}
