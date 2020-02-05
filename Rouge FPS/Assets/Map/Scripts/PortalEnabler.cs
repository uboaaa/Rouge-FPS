using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Portal = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Portal.activeSelf == true) return;

        //スポーン地点の半径15内にEnemyオブジェクトがある場合、敵はでない
        Collider[] _coll = Physics.OverlapSphere(this.transform.position, 10.0f);
        foreach (Collider c in _coll)
        {
            string _tag = c.gameObject.tag;
            //EnemyかPlayerがいる場合
            if (_tag == "Enemy")
            {
                return;
            }
        }

        m_Portal.SetActive(true);
    }
}
