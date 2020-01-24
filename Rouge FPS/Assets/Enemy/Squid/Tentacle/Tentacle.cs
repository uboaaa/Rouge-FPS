using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public GameObject Smoke = null;
    private float Tentacle_Y = 0;
    private float i = 0;

    // マテリアル
    private Material material = null;
    // シェーダパラメータ管理用
    // Hue
    private int propID_h = 0;

    public GameObject Squid = null;
    private Squid SquidSc = null;
    private EnemyParameter ep;

    // Start is called before the first frame update
    void Start()
    {
        Squid = GameObject.Find("Squid");
        SquidSc = Squid.GetComponent<Squid>();

        ep = Squid.GetComponent<EnemyParameter>();
        // マテリアル
        material = this.gameObject.GetComponent<SpriteRenderer>().material;
        propID_h = Shader.PropertyToID("_Hue");


        Tentacle_Y = -7.0f;
        i = 0.3f;

        GameObject smoke = Instantiate(Smoke) as GameObject;
        smoke.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        Destroy(smoke, 4.0f);

    }

    // Update is called once per frame
    void Update()
    {
        ep = Squid.GetComponent<EnemyParameter>();

   
        if(ep.hp <= (SquidSc.GetHp()/2)){
            material.SetFloat(propID_h, 0.45f);
        }
     


        Tentacle_Y += i;
        if(Tentacle_Y > 5.0f)
        {
            // 反転
            i = -0.12f;
        }
        if(Tentacle_Y < -7.0f)
        {
            Destroy(this.gameObject);
        }

        this.gameObject.transform.position = new Vector3(this.transform.position.x, Tentacle_Y, this.transform.position.z);

    }
}
