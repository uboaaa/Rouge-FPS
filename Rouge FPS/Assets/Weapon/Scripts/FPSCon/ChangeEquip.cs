
using UnityEngine;
using System.Collections;

public class ChangeEquip : MonoBehaviour
{
    [SerializeField]
    public GameObject Weapon1;
    [SerializeField]
    public GameObject Weapon2;

    public int ownGun;          // 1:メイン 2:サブ


    // Use this for initialization
    void Start()
    {
        ownGun = 1;
        Weapon1.SetActive(true);
        Weapon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeapon();
        }

    }

    void ChangeWeapon()
    {
        if (Weapon2.activeSelf)
        {
            ownGun = 1;
            Weapon2.SetActive(false);
            Weapon1.SetActive(true);

        }
        else
        {
            ownGun = 2;
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);

        }

    }
}
  