using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Enemy = null;

    [SerializeField]
    private int m_SpawnNum = 2;

    [SerializeField]
    private float m_SpawnRange = 1.0f;

    [SerializeField]
    private float m_SpawnInterval = 20.0f;

    private float m_timeElapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間を加算
        m_timeElapsed += Time.deltaTime;

        if (m_timeElapsed > m_SpawnInterval)
        {
            Instantiate(m_Enemy, new Vector3(0, 10, 0), new Quaternion());

            m_timeElapsed = 0.0f;
        }
    }
}
