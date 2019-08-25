2
3
4
5
6
7
8
9
10
11
12
13
14
15
16
17
18
19
20
21
22
23
24
25
26
27
28
29
30
31
32
33
34
35
36
37
38
39
40
41
42
43
44
45
46
47
 
using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{

    [SerializeField]
    private GameObject[] weapons;
    private int equipment;
    private ProcessCharaAnimEvent processCharaAnimEvent;
    private CharacterScript characterScript;

    // Use this for initialization
    void Start()
    {
        characterScript = GetComponentInParent<CharacterScript>();
        processCharaAnimEvent = transform.root.GetComponent<ProcessCharaAnimEvent>();

        //　初期装備設定
        equipment = 0;
        weapons[equipment].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")
            && characterScript.GetState() == CharacterScript.MyState.Normal)
        {
            ChangeWeapon();
        }
    }

    void ChangeWeapon()
    {
        equipment++;
        if (equipment >= weapons.Length)
        {
            equipment = 0;
        }
        //　武器を切り替え
        for (var i = 0; i < weapons.Length; i++)
        {
            if (i == equipment)
            {
                weapons[i].SetActive(true);
                processCharaAnimEvent.SetCollider(weapons[i].GetComponent<Collider>());
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }
}