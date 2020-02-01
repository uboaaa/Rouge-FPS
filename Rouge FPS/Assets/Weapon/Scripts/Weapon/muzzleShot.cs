using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleShot : MonoBehaviour
{
    [SerializeField] Transform  muzzle;                                             // 角度を取得するマズル
    [SerializeField] GameObject bulletPrefab;                                       // 弾のPrefab
    [SerializeField] Vector3    bulletScale = new Vector3(1.0f,1.0f,1.0f);          // 弾の大きさ変更用

    GunController GCScript;
    void Start()
    {
        GameObject parentObject = transform.parent.gameObject;
        GCScript = parentObject.GetComponent<GunController>();
    }

    public void Shot()
    {
        this.transform.rotation = muzzle.transform.rotation;
        if (bulletPrefab != null)
        {
            // 弾の生成
	        GameObject bullet = Instantiate<GameObject>(bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.transform.localScale = bulletScale;
		    bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * GCScript.bulletPower);
	        Destroy(bullet, 10.0f);
        }
    }
}
